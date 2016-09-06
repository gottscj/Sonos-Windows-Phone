using System.Collections.Generic;
using System.Xml.Linq;
using GalaSoft.MvvmLight.Messaging;
using Sonos;
using Sonos.Extensions;
using Sonos.Models;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class SameArtistViewModel : AlbumListViewModel
    {
        public SameArtistViewModel(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi,
            IMessenger messageBus)
            : base(navigator, zoneService, sonosMusicApi, messageBus)
        {
            
        }

        public override IEnumerable<Album> Query(XElement e)
        {
            var allElement = new XElement(CurrentContainer.XElement);
            allElement.SetTitle("All tracks");
            var all = new Album(allElement, ZoneService.CoordinatorAddress);
            HeaderItem = all;

            return base.Query(e);
        }
    }
}