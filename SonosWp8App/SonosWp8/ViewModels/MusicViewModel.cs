using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Sonos;
using Sonos.Models;
using Sonos.Models.MessageBus;
using SonosWp8.Navigation;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class MusicViewModel : ViewModelBaseExt
    {
        private ObservableCollection<ContainerWithImage> _items;

        public MusicViewModel(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi,
            IMessenger messageBus) : base(navigator, zoneService, sonosMusicApi, messageBus)
        {
            MessageBus.Register <CoordinatorChangedMessage>(this, CoordinatorChanged);
            var sonosFavorites = ContainerFactory.Create("FV:2", "", "Sonos Favorites",
                RouteSonosWp8.Favorites.Id,
                "/Assets/Dark/favs.png" /*/Assets/favorites-logo.jpg"*/);

            var sonosPlaylists = ContainerFactory.Create("SQ:", "", "Sonos Playlists",
                RouteSonosWp8.SonosPlaylists.Id,
                "/Assets/Dark/appbar.list.png" /*"/Assets/playlist-icon.png"*/);

            var musicLibrary = ContainerFactory.Create("A:", "", "Music Library",
                RouteSonosWp8.Library.Id, "/Assets/Dark/appbar.music.png" /*"/Assets/library-logo.png"*/);

            Items.Add(sonosFavorites);
            Items.Add(musicLibrary);
            Items.Add(sonosPlaylists);
        }

        public ObservableCollection<ContainerWithImage> Items
        {
            get { return _items ?? (_items = new ObservableCollection<ContainerWithImage>()); }
            set
            {
                _items = value;
                NotifyPropertyChanged("Items");
            }
        }

        private void CoordinatorChanged(CoordinatorChangedMessage e)
        {
            // we dont need this anymore
            MessageBus.Unregister<CoordinatorChangedMessage>(this);
            Task.Factory.StartNew(async () =>
            {
                var availableServices = await SonosMusicApi.GetAvailableMusicServices();

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    foreach (var service in availableServices)
                    {
                        Items.Add(service);
                    }
                });
            });
        }
    }
}