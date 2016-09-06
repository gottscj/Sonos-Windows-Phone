using System.Windows;
using System.Windows.Controls;

namespace SonosWp8.Views
{
    public partial class OverLay : UserControl
    {
        public OverLay()
        {
            InitializeComponent();
            LayoutRoot.Height = Application.Current.Host.Content.ActualHeight;
            LayoutRoot.Width = Application.Current.Host.Content.ActualWidth;
        }
    }
}