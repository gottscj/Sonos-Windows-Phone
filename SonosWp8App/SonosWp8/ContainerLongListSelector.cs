using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Sonos.Models;

namespace SonosWp8
{
    public class ContainerLongListSelector : LongListSelector
    {
        public static DependencyProperty LoadMoreDataCommandProperty
            = DependencyProperty.Register(
                "LoadMoreDataCommand",
                typeof (ICommand),
                typeof (ContainerLongListSelector), null);

        public static DependencyProperty SelectedItemChangedProperty
            = DependencyProperty.Register(
                "SelectedItemChangedCommand",
                typeof (ICommand),
                typeof (ContainerLongListSelector), null);

        public static DependencyProperty ScrollToItemProperty
            = DependencyProperty.Register(
                "ScrollToItem",
                typeof (Container),
                typeof (ContainerLongListSelector), new PropertyMetadata(ScrollToItemChanged));

        private const int OffsetKnob = 32;

        public ContainerLongListSelector()
        {
            SelectionChanged += LongListSelector_SelectionChanged;
            ItemRealized += BaseModelLongListSelector_ItemRealized;
        }

        public ICommand LoadMoreDataCommand
        {
            get { return (ICommand) GetValue(LoadMoreDataCommandProperty); }
            set { SetValue(LoadMoreDataCommandProperty, value); }
        }

        public ICommand SelectedItemChangedCommand
        {
            get { return (ICommand) GetValue(SelectedItemChangedProperty); }
            set { SetValue(SelectedItemChangedProperty, value); }
        }

        public Container ScrollToItem
        {
            get { return (Container) GetValue(ScrollToItemProperty); }
            set { SetValue(ScrollToItemProperty, value); }
        }

        private static void ScrollToItemChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            var cb = e.NewValue as Container;
            if (cb == null) return;

            var me = dpo as ContainerLongListSelector;
            if (me == null) return;
            if (me.ItemsSource == null) return;

            foreach (var item in me.ItemsSource)
            {
                var c = item as Container;
                if (c == null) return;
                if (c.Id != cb.Id) continue;
                me.ScrollTo(item);
                break;
            }
        }

        //private void OnItemUnrealized(object sender, ItemRealizationEventArgs e)
        //{
        //    if (this.ItemsSource == null) return;
        //    if (e.ItemKind != LongListSelectorItemKind.Item) return;
        //    if (!(e.Container.Content is ContainerWithImage)) return;
        //    var container = e.Container.Content as ContainerWithImage;
        //    container.ImageUri = null;
        //    //TaskScheduler.Remove((e.Container.Content as ContainerWithImage).ImageUri);
        //}

        private void BaseModelLongListSelector_ItemRealized(object sender,
            ItemRealizationEventArgs e)
        {
            if (ItemsSource == null) return;
            if (ItemsSource.Count < OffsetKnob) return;
            if (e.ItemKind != LongListSelectorItemKind.Item) return;
            if (LoadMoreDataCommand == null) return;
            if (!LoadMoreDataCommand.CanExecute(null)) return;
            
            var realizedItem = e.Container.Content as Container;
            if (realizedItem == null) return;
            var count = ItemsSource.Count;

            var triggerItem = ItemsSource[count - OffsetKnob] as Container;
            if (triggerItem == null) return;

            if (realizedItem.Id == triggerItem.Id)
            {
                LoadMoreDataCommand.Execute(count);
            }
        }

        private void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItem == null) return;
            if (SelectedItemChangedCommand == null) return;
            if (!SelectedItemChangedCommand.CanExecute(null)) return;

            SelectedItemChangedCommand.Execute(SelectedItem);

            SelectedItem = null;
        }
    }
}