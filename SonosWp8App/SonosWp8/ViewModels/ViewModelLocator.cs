using System;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Net.NetworkInformation;
using Sonos;
using Sonos.Models;
using SonosWp8.Navigation;
using IRouteProvider = SonosWp8.Navigation.IRouteProvider;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private MainPageViewModel _mainViewModel;
        private MusicViewModel _musicViewModel;
        private QueueViewModel _queueViewModel;
        private SearchViewModel _searchViewModel;

        private Navigator _navigator;
        private PersistentStorage _storage;
        private ZoneService _zoneService;
        private RouteProvider _routeProvider;

        public ViewModelLocator()
        {
            
        }

        public void Initialize(PhoneApplicationFrame rootFrame)
        {
            // init navigation

            _routeProvider = new RouteProvider(RouteSonosWp8.Unknown);
            _navigator = new Navigator(_routeProvider, rootFrame);

            var networkName = GetNetWorkName();
            // use network as prefix. this will enable caching of different networks
            _storage = new PersistentStorage(networkName);
            // save the current network in storage.
            _storage.Save("network", networkName);
            _zoneService = new ZoneService(Messenger.Default);

            // singletons
            ServiceFactory.Register<IMessenger, IMessenger>(() => Messenger.Default);
            ServiceFactory.Register<IRouteProvider, RouteProvider>(() => _routeProvider);
            ServiceFactory.Register<Navigator, Navigator>(() => _navigator);
            ServiceFactory.Register<IStorage, PersistentStorage>(() => _storage);
            ServiceFactory.Register<ZoneService, ZoneService>(() => _zoneService);
            ServiceFactory.Register<ISonosMusicApi, SonosMusicApi>(() => new SonosMusicApi(_zoneService, _storage));

        }

        public MainPageViewModel MainViewModel
        {
            get
            {
                if (_mainViewModel != null) return _mainViewModel;

                _mainViewModel = new MainPageViewModel(_navigator, _zoneService,
                    new SonosMusicApi(_zoneService, _storage), Messenger.Default);
                return _mainViewModel;
            }
        }

        public QueueViewModel QueueViewModelViewModel
        {
            get
            {
                if (_queueViewModel != null) return _queueViewModel;

                _queueViewModel = new QueueViewModel(_navigator, _zoneService,
                    new SonosMusicApi(_zoneService, _storage), Messenger.Default);
                return _queueViewModel;
            }
        }

        public MusicViewModel MusicViewModel
        {
            get
            {
                if (_musicViewModel != null) return _musicViewModel;

                _musicViewModel = new MusicViewModel(_navigator, _zoneService,
                    new SonosMusicApi(_zoneService, _storage), Messenger.Default);
                return _musicViewModel;
            }
        }

        public SearchViewModel SearchViewModel
        {
            get
            {
                if (_searchViewModel != null) return _searchViewModel;

                _searchViewModel = new SearchViewModel(_navigator, _zoneService,
                    new SonosMusicApi(_zoneService, _storage), Messenger.Default);
                return _searchViewModel;
            }
        }
        /// <summary>
        ///     NavigationDataContext is dependend on NavigationService to get information
        ///     about current routing configuration.
        /// </summary>
        public object NavigationDataContext
        {
            get
            {
                var route = _navigator.RouteInContext;
                return route.Type == null
                    ? null
                    : Activator.CreateInstance(route.Type, new object[]
                    {
                        _navigator, _zoneService,
                        new SonosMusicApi(_zoneService, _storage), Messenger.Default
                    });
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        private static string GetNetWorkName()
        {
            var networkName = "";
            var wirelessNetwork = new NetworkInterfaceList()
                .FirstOrDefault(n =>
                    n.InterfaceType == NetworkInterfaceType.Wireless80211 &&
                    n.InterfaceState == ConnectState.Connected);

            if (wirelessNetwork != null)
                networkName = wirelessNetwork.InterfaceName;

            return networkName;
        }
    }
}