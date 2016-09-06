using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace SonosWp8.Views
{
    public partial class MusicView : PhoneApplicationPage
    {
        public MusicView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var loaded = DataContext as ILoaded;
            if (loaded != null) loaded.Load();
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (DataContext as ILoaded != null) (DataContext as ILoaded).Unload();
            base.OnNavigatingFrom(e);
        }
    }
}