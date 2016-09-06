using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Sonos;
using Sonos.Models;
using SonosWp8.Extensions;
using Navigator = SonosWp8.Navigation.Navigator;

namespace SonosWp8.ViewModels
{
    public class BaseViewModel<TContainer> : ViewModelBaseExt where TContainer : Container
    {
        protected Container CurrentContainer;
        private TContainer _headerItem;
        private ObservableCollection<TContainer> _items;
        private string _title;
        protected ActionResult ActionResult;

        public BaseViewModel(Navigator navigator,
            ZoneService zoneService,
            ISonosMusicApi sonosMusicApi,
            IMessenger messageBus) : base(navigator, zoneService, sonosMusicApi, messageBus)
        {

        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        public TContainer HeaderItem
        {
            get { return _headerItem; }
            set
            {
                _headerItem = value;
                NotifyPropertyChanged("HeaderItem");
            }
        }

        public ObservableCollection<TContainer> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                NotifyPropertyChanged("Items");
            }
        }

        public ICommand LoadMoreDataCommand
        {
            get { return new RelayCommand<object>(async o => await OnLoadMoreData((int) o)); }
        }

        protected virtual async Task OnLoadMoreData(int offset)
        {
            if (offset >= ActionResult.TotalMatches) return;

            Debug.WriteLine("OnLoadMoreData");
            var elements = await GetData(CurrentContainer, (uint) offset);
            if (Items == null) return;
            if (elements == null) return;

            var containers = Query(elements);
            foreach (var container in containers)
            {
                Items.Add(container);
            }
        }

        protected override async Task OnNavigatedTo(Container container)
        {
            // if items == null, this is the first time we load 
            // if this.Title != container.Title we have same type, but different container.
            if (Items != null &&
                CurrentContainer == container)
                return;

            HeaderItem = null;
            CurrentContainer = container;

            Title = container.Title;
            Debug.WriteLine("OnNavigatedTo: {0}", GetType().Name);
            // get child objects

            var data = await GetData(container);

            if (data != null)
            {
                Items = Query(data).ToObservableCollection();
            }

            if (Items != null && Items.Count == 0)
            {
                Items.Add(
                    ContainerFactory.Create<TContainer>(container.Id, container.ParentId, "List is empty...",
                        container.ClassId));
            }
            await base.OnNavigatedTo(container);
        }

        protected override void OnNavigatedFrom()
        {
            if (Items == null) return;
            Items.Clear();
            Items = null;
        }

        /// <summary>
        ///     Creates query from XElement. Should be overriden by childs
        /// </summary>
        /// <param name="e"></param>
        /// <returns>list of models to bindable collection</returns>
        public virtual IEnumerable<TContainer> Query(XElement e)
        {
            var query = from c in e.Elements()
                select (TContainer) new Container(c);

            return query;
        }

        public virtual async Task<XElement> GetData(Container container, uint offset = 0)
        {
            using (Loader.Loading("Loading..."))
            {
                ActionResult = await ZoneService.Browse(container.Id, offset, 64)
                    .ConfigureAwait(false);    
            }
            return ActionResult.Exception != null ? null : ActionResult.Result;
        }
    }
}