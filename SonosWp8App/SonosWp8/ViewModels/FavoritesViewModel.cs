using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using GalaSoft.MvvmLight.Messaging;
using Sonos;
using Sonos.Models;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class FavoritesViewModel : BaseViewModel<Favorites>
    {
        public FavoritesViewModel(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi,
            IMessenger messageBus) : base(navigator, zoneService, sonosMusicApi, messageBus)
        {  
            
        }
        public override IEnumerable<Favorites> Query(XElement e)
        {
            var query = from element in e.Elements()
                select new Favorites(element, ZoneService.CoordinatorAddress);

            return query;
        }
    }
}