using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using GalaSoft.MvvmLight.Messaging;
using Sonos;
using Sonos.Devices;
using Sonos.Extensions;
using Sonos.Models;
using SonosWp8.Extensions;
using SonosWp8.Models;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class ServicesViewModel : BaseViewModel<MusicServiceContainer>
    {
        private ActionResult _actionResult;
        private MusicServiceContainer _currentMusicServiceContainer;
        public ServicesViewModel(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi,
            IMessenger messageBus) : base(navigator, zoneService, sonosMusicApi, messageBus)
        {  
            
        }
        protected override async Task OnNavigatedTo(Container container)
        {
            // if items == null, this is the first time we load 
            // if this.Title != container.Title we have same type, but different container.
            if (Items != null &&
                CurrentContainer == container)
                return;
            _currentMusicServiceContainer = ContainerFactory.Create<MusicServiceContainer>(container);
            CurrentContainer = _currentMusicServiceContainer;

            Items = null;

            Title = _currentMusicServiceContainer.Title;

            // get child objects
            var data = await GetData(container);
            if (data != null)
            {
                Items = Query(data).ToObservableCollection();
            }

            if (Items == null)
            {
                Items = new ObservableCollection<MusicServiceContainer>();
            }
            if (Items.Count == 0)
            {
                Items.Add(
                    ContainerFactory.Create<MusicServiceContainer>(container.Id, container.ParentId, "List is empty...", container.ClassId));
            }
        }

        protected override async Task OnLoadMoreData(int offset)
        {
            if (_actionResult != null &&
                offset >= _actionResult.TotalMatches) return;

            var apiDevice = SonosMusicApi.GetMusicApiDevice(_currentMusicServiceContainer);
            var memStorage = GetMemoryStorage(apiDevice.FriendlyName);

            ActionResult actionResult;
            using (Loader.Loading("Loading 64 items"))
            {
                if (_currentMusicServiceContainer == null) return;
                actionResult = await apiDevice
                    .MusicApiService
                    .GetMetadata(_currentMusicServiceContainer.Token, offset, 64);
            }
            _actionResult = actionResult;
            if (actionResult.Exception != null) return;
            if (offset >= actionResult.TotalMatches) return;

            var loadedData = memStorage.Load<XElement>(_currentMusicServiceContainer.Token);
            if (loadedData != null)
            {
                loadedData.Add(actionResult.Result.Elements());
                memStorage.Save(_currentMusicServiceContainer.Token, loadedData);
            }

            var query = Query(actionResult.Result);

            foreach (var item in query)
            {
                Items.Add(item);
            }
        }

        public override async Task<XElement> GetData(Container container, uint offset = 0)
        {
            if (_currentMusicServiceContainer == null)
                throw new Exception("musicService MUST be typeof MusicServiceContainer");

            var apiDevice = SonosMusicApi.GetMusicApiDevice(_currentMusicServiceContainer);


            var loadedData = GetMemoryStorage(apiDevice.FriendlyName)
                .Load<XElement>(_currentMusicServiceContainer.Token);
            if (loadedData != null) return loadedData;


            // we need to check credentials and ask user to enable service if we dont have the 
            // service credentials
            var credentialsOk = await apiDevice
                .MusicApiService.CheckCredentials()
                .ConfigureAwait(false);

            if (!credentialsOk)
            {
                if (!await TryGetCredentialsFromUser(apiDevice, _currentMusicServiceContainer.Auth)
                    .ConfigureAwait(false))
                {
                    Items = new ObservableCollection<MusicServiceContainer>
                    {
                        ContainerFactory.Create(container.Id, container.ParentId,
                            "No credentials for " + _currentMusicServiceContainer.Title,
                            container.ClassId) as MusicServiceContainer
                    };
                    return null;
                }
            }

            ActionResult actionResult;
            using (Loader.Loading("Loading..."))
            {
                actionResult = await apiDevice
                    .MusicApiService
                    .GetMetadata(_currentMusicServiceContainer.Token, 0, 64)
                    .ConfigureAwait(false);
            }
            Debug.WriteLine(actionResult.Result);
            _actionResult = actionResult;
            // disable cache for services.
            if (actionResult.Exception != null)
            {
                Items = new ObservableCollection<MusicServiceContainer>
                {
                    ContainerFactory.Create<MusicServiceContainer>(container.Id, container.ParentId,
                        "Error getting items...", container.ClassId)
                };
            }

            GetMemoryStorage(apiDevice.FriendlyName)
                .Save(_currentMusicServiceContainer.Token, actionResult.Result);
            return actionResult.Result;
        }

        public override IEnumerable<MusicServiceContainer> Query(XElement e)
        {
            const string ns = ServiceTypes.SonosServices;

            var modelsList = new List<MusicServiceContainer>();

            var metaDataCollection = e.Descendants(XName.Get("mediaCollection", ns))
                .ToList();

            if (metaDataCollection.Any())
            {
                foreach (var service in metaDataCollection)
                {
                    var imageUri = service.TryGetElementValue("albumArtURI", ns);
                    var title = service.TryGetElementValue("title", ns);
                    var id = service.TryGetElementValue("id", ns);
                    var mService = ContainerFactory.Create<MusicServiceContainer>(_currentMusicServiceContainer.XElement);
                    mService.SetId(id);
                    mService.SetImageUri(imageUri);
                    mService.SetTitle(title);
                    mService.SetToken(id);
                    modelsList.Add(mService);
                }
                return modelsList;
            }

            // no mediaCollection, try mediaMetadata
            metaDataCollection = e.Descendants(XName.Get("mediaMetadata", ns))
                .ToList();

            if (!metaDataCollection.Any()) return null;
            foreach (var service in metaDataCollection)
            {
                var imageUri = "";
                var metaData = service.Element(XName.Get("trackMetadata", ns));
                if (metaData != null)
                {
                    imageUri = metaData.TryGetElementValue("albumArtURI", ns);
                }
                else
                {
                    metaData = service.Element(XName.Get("streamMetadata", ns));
                    if (metaData != null)
                    {
                        imageUri = metaData.TryGetElementValue("logo", ns);
                    }
                }

                var title = service.TryGetElementValue("title", ns);
                var id = service.TryGetElementValue("id", ns);

                var mService = ContainerFactory.Create<MusicServiceContainer>(_currentMusicServiceContainer.XElement);
                mService.SetId(id);
                mService.SetImageUri(imageUri);
                mService.SetTitle(title);
                mService.SetToken(id);
                modelsList.Add(mService);
            }
            return modelsList;
        }

        private async Task<bool> TryGetCredentialsFromUser(SonosMusicApiDevice device, Auth auth)
        {
            if (auth != Auth.DeviceLink) return false;

            var householdId = await ZoneService.GetHouseholdId();
            var deviceLink = await device.MusicApiService.GetDeviceLinkCode(householdId);
            if (deviceLink == null) return false;
            var shouldPoll = true;

            MessageBus.Send(new DeviceAuthTokenNeededEventArgs(true, deviceLink.Url));
            MessageBus.Register<DeviceAuthTokenNeededEventArgs>(this, e => shouldPoll = e.IsNeeded);
            DeviceAuthToken deviceAuthToken;
            do
            {
                await Task.Delay(2000); // poll every 2nd second
                deviceAuthToken = await device.MusicApiService.GetDeviceAuthToken(householdId, deviceLink.LinkCode);
            } while (deviceAuthToken == null && shouldPoll);

            MessageBus.Unregister<DeviceAuthTokenNeededEventArgs>(this);
            MessageBus.Send(new DeviceAuthTokenNeededEventArgs(false, ""));
            return deviceAuthToken != null;
        }
    }
}