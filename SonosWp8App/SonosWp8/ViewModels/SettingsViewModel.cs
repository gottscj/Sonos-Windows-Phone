using GalaSoft.MvvmLight.Messaging;
using Sonos;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class SettingsViewModel : ViewModelBaseExt
    {
        public SettingsViewModel(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi,
            IMessenger messageBus) : base(navigator, zoneService, sonosMusicApi, messageBus)
        {  
            
        }
    }
}