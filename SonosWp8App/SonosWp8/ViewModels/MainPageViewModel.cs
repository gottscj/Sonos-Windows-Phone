using GalaSoft.MvvmLight.Messaging;
using Sonos;
using SonosWp8.Models.MessageBus;
using SonosWp8.Navigation;

namespace SonosWp8.ViewModels
{
    public class MainPageViewModel : ViewModelBaseExt
    {
        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainPageViewModel(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi,
            IMessenger messageBus) : base(navigator, zoneService, sonosMusicApi, messageBus)
        {
            MessageBus.Register<MainPageLoadedMessage>(this, OnReady);
        }

        private async void OnReady(MainPageLoadedMessage e)
        {
            bool initsucces;
            using (Loader.Loading("Searching for zones..."))
            {
                initsucces = await ZoneService.Initialize();
            }

            if (!initsucces)
            {
                MessageBus.Send(new LoadingEnableMessage
                {
                    Message = "Could not find any zone(s), please restart app and try again..."
                });
            }
                

        }
    }
}