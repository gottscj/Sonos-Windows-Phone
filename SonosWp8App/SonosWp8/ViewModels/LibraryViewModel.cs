using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using GalaSoft.MvvmLight.Messaging;
using Sonos;
using Sonos.Models;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class LibraryViewModel : BaseViewModel<Container>
    {
        public LibraryViewModel(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi,
            IMessenger messageBus) : base(navigator, zoneService, sonosMusicApi, messageBus)
        {  
            
        }
        public override IEnumerable<Container> Query(XElement e)
        {
            var query = from c in e.Elements()
                let m = new Container(c)
                where m.Title != "Contributing Artists" &&
                      m.Title != "Composers"
                select m;
            return query;
        }
    }
}