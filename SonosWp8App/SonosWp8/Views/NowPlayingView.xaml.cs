using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Phone.Controls;
using Sonos;
using Sonos.Models;
using Sonos.Models.MessageBus;

namespace SonosWp8.Views
{
    public partial class NowPlayingView : PhoneApplicationPage
    {
        private const string MuteSoundIconUri = "/Assets/Dark/appbar.sound.mute.png";
        private const string Sound1IconUri = "/Assets/Dark/appbar.sound.1.png";
        private const string Sound2IconUri = "/Assets/Dark/appbar.sound.2.png";
        private const string Sound3IconUri = "/Assets/Dark/appbar.sound.3.png";
        private readonly IMessenger _messageBus = ServiceFactory.Get<IMessenger>();
        private readonly ZoneService _zoneService = ServiceFactory.Get<ZoneService>();
        private bool _currentMute;
        private int _currentVolume;


        public NowPlayingView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext as ILoaded != null) (DataContext as ILoaded).Load();
            
            SetVolumeUri(_zoneService.CurrentVolume);
            VolumeSlider.Value = _zoneService.CurrentVolume.Volume;
            _currentMute = _zoneService.CurrentVolume.IsMuted;

            _messageBus
                .Register<VolumeChangedMessage>(this,
                    m => DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        if ((int) VolumeSlider.Value == m.ZoneVolume.Volume) return;
                        VolumeSlider.Value = m.ZoneVolume.Volume;
                        // _currentVolume will be set in SliderValue changed callback
                        _currentMute = m.ZoneVolume.IsMuted;
                        SetVolumeUri(m.ZoneVolume);
                    }));
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (DataContext as ILoaded != null) (DataContext as ILoaded).Unload();
            _messageBus.Unregister(this);

            base.OnNavigatingFrom(e);
        }


        private void VolumeSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _currentVolume = (int) e.NewValue;
            SetVolumeUri(_currentVolume);
        }

        private void VolumeSlider_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _zoneService.SetVolume(_currentVolume);
        }

        private void Mute_Tap(object sender, RoutedEventArgs e)
        {
            _currentMute = !_currentMute;
            SetVolumeUri(_currentVolume);
            _zoneService.SetMute(_currentMute);
        }


        private void SetVolumeUri(int volume)
        {
            var image = VolumeButton.Content as Image;
            if (volume == 0 || _currentMute)
            {
                image.Source = new BitmapImage(new Uri(MuteSoundIconUri, UriKind.Relative));
            }
            else if (volume <= 33)
            {
                image.Source = new BitmapImage(new Uri(Sound1IconUri, UriKind.Relative));
            }
            else if (volume <= 66)
            {
                image.Source = new BitmapImage(new Uri(Sound2IconUri, UriKind.Relative));
            }
            else
            {
                image.Source = new BitmapImage(new Uri(Sound3IconUri, UriKind.Relative));
            }
        }

        private void SetVolumeUri(ZoneVolume zoneVolume)
        {
            int volume = zoneVolume.IsMuted ? 0 : zoneVolume.Volume;
            SetVolumeUri(volume);
        }
    }
}