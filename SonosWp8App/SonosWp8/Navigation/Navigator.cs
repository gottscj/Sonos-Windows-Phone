using System;
using System.ComponentModel;
using System.Windows.Navigation;
using System.Xml.Linq;
using Microsoft.Phone.Controls;
using Sonos;
using Sonos.Models;

namespace SonosWp8.Navigation
{
    public class Navigator
    {
        private readonly PhoneApplicationFrame _rootFrame;
        private readonly SonosWp8.Navigation.IRouteProvider _routeProvider;
        private NavigationMode _navigationMode;

        public Navigator(SonosWp8.Navigation.IRouteProvider routeProvider, PhoneApplicationFrame rootFrame)
        {
            _routeProvider = routeProvider;

            _rootFrame = rootFrame;
            if (_rootFrame == null)
            {
                throw new Exception("Application.Current.RootVisual is not of type PhoneApplicationFrame");
            }
            _rootFrame.BackKeyPress += OnBackKeyPress;
            _rootFrame.Navigating += _rootFrame_Navigating;
        }

        public object NavigationContext { get; private set; }

        public object LastNavigatedContext { get; private set; }

        public SonosWp8.Navigation.IRouteMap RouteInContext
        {
            get
            {
                return _routeProvider.GetRouteById(((Container)NavigationContext).ClassId); 
            }   
        }

        public NavigationMode NavigationMode
        {
            get { return _navigationMode; }
        }

        public event EventHandler<CancelEventArgs> NavigateBackInterceptor;

        private void _rootFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            var index = e.Uri.OriginalString.IndexOf("?", StringComparison.Ordinal);
            if (index == -1) return;
            index += 2;
            var query = e.Uri.OriginalString.Substring(index);
            var xml = XElement.Parse(query);

            _navigationMode = e.NavigationMode;

            LastNavigatedContext = NavigationContext;
            NavigationContext = ContainerFactory.Create(xml);
        }

        private void OnBackKeyPress(object sender, CancelEventArgs e)
        {
            if (NavigateBackInterceptor != null)
            {
                NavigateBackInterceptor(this, e);
                if (e.Cancel) return;
            }

            if (_rootFrame.CanGoBack)
            {
                _rootFrame.GoBack();
            }
        }

        public void Navigate(Container container)
        {
            if (container == null) return;

            var routingConfig = _routeProvider.GetRouteById(container.ClassId);

            if (routingConfig == null) return;
            var query = container.XElement.ToString();

            var uri = new Uri(routingConfig.Uri + "?=" + query, UriKind.Relative);

            _rootFrame.Navigate(uri);
        }
    }
}