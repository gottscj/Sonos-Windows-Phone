using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using GalaSoft.MvvmLight.Messaging;
using Sonos;
using Sonos.Models;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class AlbumListViewModel : BaseViewModel<Album>
    {
        public AlbumListViewModel(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi,
            IMessenger messageBus) : base(navigator, zoneService, sonosMusicApi, messageBus)
        {  
            
        }
        public override IEnumerable<Album> Query(XElement e)
        {
            IEnumerable<Album> query = from c in e.Elements()
                select new Album(c, ZoneService.CoordinatorAddress);

            return query;
        }
    }
}