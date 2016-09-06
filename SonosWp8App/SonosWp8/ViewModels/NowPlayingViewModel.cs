using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Sonos;
using Sonos.Models;
using Sonos.Models.MessageBus;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class NowPlayingViewModel : ViewModelBaseExt
    {
        private string _albumTitle = "";
        private string _artist = "";
        private TransportState _currentTransportState;
        private string _imageUri = "";
        private string _playPauseUri = "";
        private string _title = "";
        
        private string _zoneName = "";

        public NowPlayingViewModel(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi,
            IMessenger messageBus) : base(navigator, zoneService, sonosMusicApi, messageBus)
        {  
        }
        public string ZoneName
        {
            get { return _zoneName; }
            set
            {
                _zoneName = value;
                NotifyPropertyChanged("ZoneName");
            }
        }

        public string AlbumTitle
        {
            get { return _albumTitle; }
            set
            {
                if (_albumTitle == value) return;
                _albumTitle = value;
                NotifyPropertyChanged("AlbumTitle");
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value) return;
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        public string Artist
        {
            get { return _artist; }
            set
            {
                if (_artist == value) return;
                _artist = value;
                NotifyPropertyChanged("Artist");
            }
        }

        public string ImageUri
        {
            get { return _imageUri; }
            set
            {
                if (_imageUri == value) return;
                _imageUri = value;
                NotifyPropertyChanged("ImageUri");
            }
        }

        public string PlayPauseUri
        {
            get { return _playPauseUri; }
            set
            {
                if (_playPauseUri == value) return;
                _playPauseUri = value;
                NotifyPropertyChanged("PlayPauseUri");
            }
        }

        public ICommand PlayPauseCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (_currentTransportState == null) return;

                    if (_currentTransportState == TransportState.Playing)
                        ZoneService.Pause();
                    else
                        ZoneService.Play();
                });
            }
        }

        public ICommand BackCommand
        {
            get
            {
                return
                    new RelayCommand(() => ZoneService.Previous());
            }
        }

        public ICommand NextCommand
        {
            get
            {
                return
                    new RelayCommand(() => ZoneService.Next());
            }
        }

        private readonly Task _nopTask = Task.FromResult(false);
        protected override Task OnNavigatedTo(Container container)
        {
            ZoneName = ZoneService.CoordinatorName;

            PlayPauseUri = ZoneService.CurrentTransportState == TransportState.Playing ?
                "/Assets/Dark/transport.pause.png" :
                "/Assets/Dark/transport.play.png";
            MessageBus.Register<TransportStateChangedMessage>(this, message =>
            {
                _currentTransportState = message.TransportState;
                PlayPauseUri = _currentTransportState == TransportState.Playing ? 
                    "/Assets/Dark/transport.pause.png" : 
                    "/Assets/Dark/transport.play.png";
            });

            MessageBus.Register<PositionInfoChangedMessage>(this, message =>
            {
                Title = message.PositionInfo.Track.Title;
                AlbumTitle = message.PositionInfo.Track.AlbumTitle;
                Artist = message.PositionInfo.Track.Artist;
                ImageUri = message.PositionInfo.Track.ImageUri;
            });
            return _nopTask;
        }

        protected override void OnNavigatedFrom()
        {
            MessageBus.Unregister(this);
            base.OnNavigatedFrom();
        }

       


        //private async void GetVolume()
        //{
        //    var volume = await ZoneService.GetVolume();

        //    if (_volume == null ||
        //        _volume.Volume != volume.Volume ||
        //        _volume.IsMuted != volume.IsMuted)
        //    {
        //        Debug.WriteLine("volume: {0}, ismuted: {1}", volume.Volume, volume.IsMuted.ToString());
        //        _volume = volume;
        //        MessageBus.Send<ZoneVolume>(_volume, Message.Volume);    
        //    }
        //}
    }
}