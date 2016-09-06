using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using GalaSoft.MvvmLight.Messaging;
using Sonos;
using Sonos.Models;
using Sonos.Models.MessageBus;
using SonosWp8.Extensions;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class QueueViewModel : BaseViewModel<Track>
    {
        private static string _lastQueueId = "";

        public QueueViewModel(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi,
            IMessenger messageBus) : base(navigator, zoneService, sonosMusicApi, messageBus)
        {  
            
        }
        protected override async Task OnNavigatedTo(Container container)
        {
            CurrentContainer = container;
            Title = ZoneService.CoordinatorName;
            MessageBus.Register<QueueChangedMessage>(this, QueueChanged);

            var data = await GetData(CurrentContainer);
            if (data != null)
            {
                Items = Query(data).ToObservableCollection();
            }

            if (Items.Count == 0)
            {
                Items.Add(
                    ContainerFactory.Create<Track>(CurrentContainer.Id, CurrentContainer.ParentId, "Queue is empty...",
                        CurrentContainer.ClassId));
            }
        }

        protected override void OnNavigatedFrom()
        {
            if (Items != null)
            {
                Items.Clear();
            }

            Items = null;

            MessageBus.Unregister(this);
        }

        private void QueueChanged(QueueChangedMessage message)
        {
            if (message.QueueId == _lastQueueId) return;

            _lastQueueId = message.QueueId;

            var data = GetData(CurrentContainer).Result;
            if (data == null) return;

            var items = Query(data).ToObservableCollection();
            if (Items == null || Items.Count == 0)
            {
                Items = items;
            }
            else if (items.Count == 0)
            {
                Items.Clear();
                Items.Add(
                    ContainerFactory.Create<Track>(CurrentContainer.Id, CurrentContainer.ParentId, "Queue is empty...",
                        CurrentContainer.ClassId));
            }
            else
            {
                for (var i = 0; i < Items.Count; i++)
                {
                    if (Items[i].Id == items[i].Id) continue;
                    Items = items;
                    break;
                }
            }
        }


        protected override async Task CurrentModelChange(Container container)
        {
            var i = 0;
            while (Items[i++].Id != container.Id)
            {
            }

            await ZoneService.PlayQueueIndex(i);
        }

        public override IEnumerable<Track> Query(XElement e)
        {
            var query = from c in e.Elements()
                select new Track(c, ZoneService.CoordinatorAddress);

            return query;
        }

        //protected override async void OnLoadMoreData(int offset)
        //{
        //    if (offset >= ActionResult.TotalMatches) return;

        //    var data = await GetData(CurrentContainer, (uint) offset);
        //    //ActionResult data =
        //    //    await ZoneService.CurrentCoordinator.MediaServer.ContentDirectory1Service.Browse(CurrentContainer.Id,
        //    //        BrowseFlag.BrowseDirectChildren, Filter, (uint) offset, 64, "");
        //    // user has navigated away while loading data. exit...

        //    if (Items == null) return;
        //    if (data == null) return;
        //    IEnumerable<Track> query = Query(data);
        //    foreach (Track track in query)
        //    {
        //        Items.Add(track);
        //    }
        //}

        public override async Task<XElement> GetData(Container container, uint offset = 0)
        {
            ActionResult = await ZoneService.Browse(CurrentContainer.Id, 0, 64)
                .ConfigureAwait(false);

            return ActionResult.Exception != null ? null : ActionResult.Result;
        }
    }
}