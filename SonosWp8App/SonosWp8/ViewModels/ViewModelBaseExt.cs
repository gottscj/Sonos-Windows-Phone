using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Sonos;
using Sonos.Models;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class ViewModelBaseExt : ILoaded, INotifyPropertyChanged
    {
        // services
        private Container _scrollToContainer;
        public Navigator Navigator { get; private set; }
        public ZoneService ZoneService { get; private set; }
        public ISonosMusicApi SonosMusicApi { get; private set; }
        public IMessenger MessageBus { get; private set; }

        public ViewModelBaseExt(Navigator navigator,  ZoneService zoneService, ISonosMusicApi sonosMusicApi, IMessenger messageBus)
        {
            Navigator = navigator;
            ZoneService = zoneService;
            SonosMusicApi = sonosMusicApi;
            MessageBus = messageBus;
        }
        public ICommand SelectedItemCommand
        {
            get { return new RelayCommand<object>(o => CurrentModelChange(o as Container)); }
        }

        public Container ScrollToItem
        {
            get { return _scrollToContainer; }
            set
            {
                _scrollToContainer = value;
                NotifyPropertyChanged("ScrollToItem");
            }
        }

        void ILoaded.Load()
        {
            var model = Navigator.NavigationContext as Container;
            OnNavigatedTo(model);

            if (Navigator.NavigationMode == NavigationMode.Back)
            {
                ScrollToItem = Navigator.LastNavigatedContext as Container;
            }
            else
            {
                ScrollToItem = null;
            }
        }

        void ILoaded.Unload()
        {
            OnNavigatedFrom();
            MessageBus.Unregister(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IStorage GetMemoryStorage(string prefix)
        {
            return new MemoryStorage(prefix);
        }

        protected virtual Task CurrentModelChange(Container container)
        {
            Navigator.Navigate(container);
            return _nopTask;
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() => handler(this, new PropertyChangedEventArgs(propertyName)));
            }
        }

        private readonly Task _nopTask = Task.FromResult(false);
        protected virtual Task OnNavigatedTo(Container container)
        {
            return _nopTask;
        }

        protected virtual void OnNavigatedFrom()
        {
        }
    }
}