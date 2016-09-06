using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Sonos;
using Sonos.Models;
using Sonos.Models.MessageBus;
using SonosWp8.Models;
using SonosWp8.Models.MessageBus;

namespace SonosWp8.Views
{
    public partial class RoomsView : UserControl
    {
        private readonly IMessenger _messageBus = ServiceFactory.Get<IMessenger>();
        private bool _loaded;
        private List<Action> _preLoadActions;
        private readonly ZoneService _zoneService = ServiceFactory.Get<ZoneService>();
        public RoomsView()
        {
            _preLoadActions = new List<Action>();
            _messageBus.Register<ZoneTopologyChangedMessage>(this, m => BuildRadioButtons(m.ZoneTopology.Members));
            InitializeComponent();
            Loaded += RoomsView_Loaded;
        }

        private void RoomsView_Loaded(object sender, RoutedEventArgs e)
        {
            if (_loaded) return;
            _loaded = true;
            // run pending actions.
            _preLoadActions.ForEach(a => a());
            _preLoadActions.Clear();
            _preLoadActions = null;
        }

        private void BuildRadioButtons(List<ZoneMember> zoneMembers)
        {
            if (!_loaded)
            {
                _preLoadActions.Add(() => BuildRadioButtons(zoneMembers));
                return;
            }
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(() => BuildRadioButtons(zoneMembers));
                return;
            }

            var sp = LayoutRoot;
            sp.Children.Clear();
            foreach (var zoneMember in zoneMembers)
            {
                var radiobutton = new RadioButton
                {
                    Command = new RelayCommand<string>(uuid => _zoneService.ChangeCoordinator(uuid)),
                    CommandParameter = zoneMember.Uuid,
                    GroupName = "Zones",
                    IsChecked = zoneMember.IsCoordinator
                };
                var tb = new TextBlock
                {
                    Style = Resources["PhoneTextExtraLargeStyle"] as Style, 
                    Text = zoneMember.Title
                };

                radiobutton.Content = tb;
                sp.Children.Add(radiobutton);
            }
        }
    }
}