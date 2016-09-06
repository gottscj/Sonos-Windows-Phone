using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Sonos;
using Sonos.Models;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class SearchViewModel : ViewModelBaseExt
    {
        public SearchViewModel(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi,
            IMessenger messageBus) : base(navigator, zoneService, sonosMusicApi, messageBus)
        {
            // objectId = "A:ALBUMARTIST:" + searchTerm
        }

        protected override Task OnNavigatedTo(Container container)
        {
            Title = container.Title;
            return base.OnNavigatedTo(container);
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(async searchTerm =>
                {
                    Debug.WriteLine("Search term: " + searchTerm);
                    var result = await ZoneService.Browse("A:ALBUMARTIST:" + searchTerm, 0, 1);

                    Debug.WriteLine("result: " + result.Result.ToString());
                });
            }
        }

    }
}