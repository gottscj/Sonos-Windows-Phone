using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using GalaSoft.MvvmLight.Messaging;
using Sonos;
using Sonos.Extensions;
using Sonos.Models;
using SonosWp8.Models.MessageBus;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class PlaylistViewModel : BaseViewModel<Track>
    {
        public PlaylistViewModel(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi,
            IMessenger messageBus) : base(navigator, zoneService, sonosMusicApi, messageBus)
        {  
            
        }

        private readonly Task _nopTask = Task.FromResult(false);
        protected override Task CurrentModelChange(Container container)
        {
            MessageBus.Send(new PlayOptionsEnableMessage {Container = container});
            return _nopTask;
        }

        public override IEnumerable<Track> Query(XElement e)
        {
            var allElement = new XElement(CurrentContainer.XElement);
            allElement.SetTitle("All tracks");
            var all = new Track(allElement, ZoneService.CoordinatorAddress);

            HeaderItem = all;

            var query = from c in e.Elements()
                select new Track(c, ZoneService.CoordinatorAddress);

            return query;
        }
    }
}