using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace SonosWp8.Views
{
    public partial class ImageArtistAndTitleView : PhoneApplicationPage
    {
        public ImageArtistAndTitleView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext as ILoaded != null) (DataContext as ILoaded).Load();
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (DataContext as ILoaded != null) (DataContext as ILoaded).Unload();
            base.OnNavigatingFrom(e);
        }
    }
}