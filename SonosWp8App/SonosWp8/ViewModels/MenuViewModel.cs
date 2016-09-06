using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Sonos;
using Sonos.Models;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class MenuViewModel : ViewModelBaseExt
    {
        private List<ContainerWithImage> _items;

        public MenuViewModel(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi,
            IMessenger messageBus) : base(navigator, zoneService, sonosMusicApi, messageBus)
        {
            var menuItems = new List<ContainerWithImage>
            {
                ContainerFactory.Create("", "", "Settings", "", "/Assets/Dark/feature.settings.png"),
                ContainerFactory.Create("", "", "Alarms", "", "/Assets/Dark/feature.alarm.png"),
                ContainerFactory.Create("", "", "Services", "", "/Assets/Dark/feature.alarm.png")
            };

            Items = menuItems;
        }

        public List<ContainerWithImage> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                NotifyPropertyChanged("Items");
            }
        }

        public ICommand SettingsCommand
        {
            get { return new RelayCommand(() => Navigator.Navigate(null)); }
        }

        public ICommand SleepTimerCommand
        {
            get { return new RelayCommand(() => { }); }
        }

        public ICommand AlarmsCommand
        {
            get { return new RelayCommand(() => { }); }
        }
    }
}