using GalaSoft.MvvmLight.Messaging;
using Sonos;
using Sonos.Models;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class AlarmsViewModel : BaseViewModel<Alarms>
    {
        public AlarmsViewModel(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi,
            IMessenger messageBus) : base(navigator, zoneService, sonosMusicApi, messageBus)
        {  
            
        }
    }
}