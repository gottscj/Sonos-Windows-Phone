using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sonos;
using Sonos.Models;
using Sonos.Models.MessageBus;
using SonosWp8.Models;
using SonosWp8.Models.MessageBus;
using SonosWp8.Navigation;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly IMessenger _messageBus = ServiceFactory.Get<IMessenger>();
        private readonly Navigator _navigator = ServiceFactory.Get<Navigator>();
        private readonly ApplicationBarIconButton _playButton;
        private readonly ZoneService _zoneService = ServiceFactory.Get<ZoneService>();
        private Container _currentContainer;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
            _messageBus.Register<LoadingEnableMessage>(this, SetLoadingEnable);
            _messageBus.Register<LoadingDisableMessage>(this, SetLoadingDisable);

            _messageBus.Register<DeviceAuthTokenNeededEventArgs>(this, OnDeviceAuthTokenNeeded);

            _messageBus.Register<PlayOptionsEnableMessage>(this, ShowPlayOptions);

            _messageBus.Register<TransportStateChangedMessage>(this, TransportInfoChanged);

            _playButton = ApplicationBar.Buttons.Cast<ApplicationBarIconButton>().Skip(2).Take(1).First();
            _playButton.Click += ApplicationBarButtonPlayPause;

            var nowPlayingButton =
                ApplicationBar.Buttons.Cast<ApplicationBarIconButton>().Skip(1).Take(1).First();
            nowPlayingButton.Click += NowPlayingButtonOnClick;

            var searchButton = ApplicationBar.Buttons.Cast<ApplicationBarIconButton>().First();
            searchButton.Click += SearchButtonOnClick;

            var nextButton = ApplicationBar.Buttons.Cast<ApplicationBarIconButton>().Last();
            nextButton.Click += ApplicationBarButtonNext;

            var queueMenuItem = ApplicationBar.MenuItems[0] as ApplicationBarMenuItem;
            if (queueMenuItem != null) queueMenuItem.Click += queueMenuItem_Click;

            var homeMenuItem = ApplicationBar.MenuItems[1] as ApplicationBarMenuItem;
            if (homeMenuItem != null) homeMenuItem.Click += HomeMenuItemOnClick;
        }

        private void HomeMenuItemOnClick(object sender, EventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        private void queueMenuItem_Click(object sender, EventArgs e)
        {
            const string queueParentId = "Q:"; // we dont care about this.
            const string queueTitle = "queue";

            var element = ContainerFactory.Create("Q:0", queueParentId, queueTitle, RouteSonosWp8.Queue.Id);

            _navigator.Navigate(element);
        }

        private void NowPlayingButtonOnClick(object sender, EventArgs eventArgs)
        {
            var nowPlayingContainer = ContainerFactory.Create(RouteSonosWp8.NowPlaying.Id, "", "Now Playing",
                RouteSonosWp8.NowPlaying.Id);

            _navigator.Navigate(nowPlayingContainer);
        }

        private void SearchButtonOnClick(object sender, EventArgs eventArgs)
        {
            var searchContainer = ContainerFactory.Create(RouteSonosWp8.Search.Id, "", "Search",
                RouteSonosWp8.Search.Id);

            _navigator.Navigate(searchContainer);
        }

        private void ShowPlayOptions(PlayOptionsEnableMessage message)
        {
            if (PlayPopup.IsOpen)
            {
                ClosePlayOptions();
                return;
            }
            var container = message.Container;
            _navigator.NavigateBackInterceptor += NavigateBackInterceptor;
            if (container != null)
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() => PlayTextBlock.Text = container.Title);
            }
            _currentContainer = container;
            PlayPopup.IsOpen = true;
        }

        private void ClosePlayOptions()
        {
            _navigator.NavigateBackInterceptor -= NavigateBackInterceptor;
            PlayPopup.IsOpen = false;
        }

        private void NavigateBackInterceptor(object sender, CancelEventArgs cancelEventArgs)
        {
            // cancel the go back event.
            cancelEventArgs.Cancel = true;
            // close the popup
            PlayPopup.IsOpen = false;

            // disable this interceptor;
            _navigator.NavigateBackInterceptor -= NavigateBackInterceptor;
        }

        private void SetLoadingEnable(LoadingEnableMessage message)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                Panorama.Opacity = 0.2;
                ApplicationBar.IsVisible = false;
                OverLayPopup.IsOpen = true;
                LoadingTextBlock.Text = message.Message;
            });
        }

        private void SetLoadingDisable(LoadingDisableMessage message)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                Panorama.Opacity = 1.0;
                ApplicationBar.IsVisible = true;
                OverLayPopup.IsOpen = false;
                LoadingTextBlock.Text = "";
            });
        }

        private void OnDeviceAuthTokenNeeded(DeviceAuthTokenNeededEventArgs e)
        {
            // if true we need to show webbrowser
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                var grid = WebBrowserPopup.Child as Grid;
                if (grid == null)
                    throw new Exception("Expectd child to be typeof grid");

                grid.Height = Application.Current.Host.Content.ActualHeight;
                grid.Width = Application.Current.Host.Content.ActualWidth;
                var browser = grid.Children.OfType<WebBrowser>()
                    .FirstOrDefault();
                if (browser == null)
                {
                    Debug.WriteLine("No Webbrowser found!");
                    return;
                }
                if (e.IsNeeded)
                {
                    WebBrowserPopup.IsOpen = true;
                    browser.IsScriptEnabled = true;
                    browser.Navigate(new Uri(e.DeviceLinkUrl, UriKind.Absolute));
                }
                WebBrowserPopup.IsOpen = false;
                browser.Navigate(new Uri("about:blank", UriKind.Absolute));
            });
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            PlayPopup.HorizontalOffset = (Application.Current.Host.Content.ActualWidth - PlayPopup.ActualWidth)/2;
            PlayPopup.VerticalOffset = (Application.Current.Host.Content.ActualHeight - PlayPopup.ActualHeight)/2;

            _messageBus.Send(new MainPageLoadedMessage());

            Loaded -= MainPage_Loaded;
        }

        private void TransportInfoChanged(TransportStateChangedMessage message)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                switch (message.TransportState)
                {
                    case TransportState.Playing:
                        _playButton.IconUri = new Uri("/Assets/Dark/transport.pause.png", UriKind.Relative);
                        break;
                    default:
                        _playButton.IconUri = new Uri("/Assets/Dark/transport.play.png", UriKind.Relative);
                        break;
                }
                
            });
        }

        private void Pivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _messageBus.Send(new DeviceAuthTokenNeededEventArgs(false, ""));
        }

        private void ApplicationBarButtonNext(object sender, EventArgs e)
        {
            _zoneService.Next();
        }

        private void ApplicationBarButtonPlayPause(object sender, EventArgs e)
        {
            if (_zoneService.CurrentTransportState == TransportState.Playing)
            {
                _zoneService.Pause();
                DispatcherHelper.CheckBeginInvokeOnUI(
                    () => _playButton.IconUri = new Uri("/Assets/Dark/transport.play.png", UriKind.Relative));
            }
            else if (_zoneService.CurrentTransportState == TransportState.Pausedplayback ||
                     _zoneService.CurrentTransportState == TransportState.Stopped)
            {
                _zoneService.Play();
                DispatcherHelper.CheckBeginInvokeOnUI(
                    () => _playButton.IconUri = new Uri("/Assets/Dark/transport.pause.png", UriKind.Relative));
            }
        }

        private void PlayNow_OnClick(object sender, RoutedEventArgs e)
        {
            ClosePlayOptions();
            if (_currentContainer == null) return;
            _zoneService.Play(_currentContainer);
        }
    }
}