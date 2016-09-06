using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Sonos.Devices;
using Sonos.Extensions;
using Sonos.Models;
using UPnP.Extensions;

namespace Sonos
{
    public class SonosMusicApi : ISonosMusicApi
    {
        private static readonly List<SonosMusicApiDevice> Apis = new List<SonosMusicApiDevice>();
        private static List<MusicServiceContainer> _musicServices;
        private static List<MusicServiceAccount> _musicServiceAccounts;
        private static XElement _serviceList;
        private static ICollection<ServicesLogo> _serviceLogos;

        private readonly IStorage _storage;
        private readonly ZoneService _zoneService;

        public SonosMusicApi(ZoneService zoneService, IStorage storage)
        {
            _storage = storage;
            _zoneService = zoneService;
        }

        public async Task<List<MusicServiceContainer>> GetAvailableMusicServices()
        {
            if (_musicServices != null) return _musicServices;

            var activeAccounts = await GetActiveServiceAccounts();
            var logos = await GetServiceLogos();

            var actionResult = await _zoneService.GetAvailableServices();

            var availableServiceDescriptorList = actionResult.Result.Element(XName.Get("AvailableServiceDescriptorList"));
            
            if (availableServiceDescriptorList != null)
                _serviceList = XElement.Parse(availableServiceDescriptorList.Value);

            var query = (from service in _serviceList.Elements()
                from logo in logos
                let serviceName = service.GetName()
                where string.Compare(logo.Name, serviceName, StringComparison.CurrentCultureIgnoreCase) == 0
                select new MusicServiceContainer(service, logo.ImageUri, logo.Id, "root")).ToList();

            var radio = _serviceList.Elements().FirstOrDefault(x => x.GetName() == "TuneIn");
            if (radio != null)
            {
                var tuneIn = new MusicServiceContainer(radio, "/Assets/tunein-logo.jpg",
                    radio.TryGetAttributeValue("Id"), "root");
                tuneIn.SetTitle("Radio");
                tuneIn.SetClassId("Radio");
                query.Add(tuneIn);
            }

            _musicServices = activeAccounts
                .Select(musicServiceAccount => 
                    query.FirstOrDefault(x => x.ServiceId == musicServiceAccount.ServiceId))
                    .Where(service => service != null)
                    .OrderBy(x => x.Title)
                    .ToList();

            var deviceId = _zoneService.CoordinatorDeviceId;
            foreach (var serviceModel in _musicServices)
            {
                var device = new SonosMusicApiDevice(this, deviceId, serviceModel, new Uri(serviceModel.Uri), _zoneService);
                Apis.Add(device);
            }
            return _musicServices;
        }

        public SonosMusicApiDevice GetMusicApiDevice(MusicServiceContainer serviceContainer)
        {
            return Apis.FirstOrDefault(x => x.MusicApiService.ControlUrl == serviceContainer.Uri);
        }

        public async Task<List<MusicServiceAccount>> GetActiveServiceAccounts()
        {
            if (_musicServiceAccounts != null) return _musicServiceAccounts;

            var musicServiceAccounts = new List<MusicServiceAccount>();

            var target = _zoneService.CoordinatorAddress + "/status/accounts";
            
            var accountsXml = await new WebClient().DownloadStringAsync(target)
                .ConfigureAwait(false);

            var accountsXElement = XElement.Parse(accountsXml);

            var accounts = accountsXElement.Descendants(XName.Get("Account"));
            foreach (var accountElement in accounts)
            {
                var id = accountElement.TryGetAttributeValue("Type");
                var username = accountElement.TryGetElementValue("UN");
                musicServiceAccounts.Add(new MusicServiceAccount(id, username));
            }
            // tunin radio manually added
            musicServiceAccounts.Add(new MusicServiceAccount("254", ""));
            _musicServiceAccounts = musicServiceAccounts;

            return musicServiceAccounts;
        }

        public async Task<string> GetHouseholdId()
        {
            var householdId = await _zoneService.GetHouseholdId();
            return householdId;
        }

        public DeviceAuthToken GetDeviceAuthToken(string serviceName)
        {
            var accessToken = _storage.Load<XElement>("accessToken_" + serviceName);
            return accessToken != null ? new DeviceAuthToken(accessToken) : null;
        }

        public void SetDeviceAuthToken(string serviceName, DeviceAuthToken deviceAuthToken)
        {
            _storage.Save("accessToken_" + serviceName, deviceAuthToken.XElement);
        }

        private async Task<ICollection<ServicesLogo>> GetServiceLogos()
        {
            if (_serviceLogos != null) return _serviceLogos;


            const string target = "http://update-services.sonos.com/services/mslogo.xml";


            var xml = await new WebClient().DownloadStringAsync(target)
                .ConfigureAwait(false);

            var availableServicesCsv = await _zoneService.GetString("R_AvailableSvcTypes");
            var availableServices = availableServicesCsv.Split(',');

            var element = XElement.Parse(xml);
            var xElement = element.Element(XName.Get("sized"));
            if (xElement == null) return _serviceLogos;

            var query = from e in xElement.Elements()
                where availableServices.Contains(e.TryGetAttributeValue("id"))
                select new ServicesLogo(e);

            _serviceLogos = query.ToList();

            return _serviceLogos;
        }
    }
}