using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Sonos.Models;
using SonosWp8.Models;
using SonosWp8.Models.MessageBus;

namespace SonosWp8
{
    class Loader : IDisposable
    {
        public static IDisposable Loading(string message)
        {
            return new Loader(message);
        }

        public Loader(string message)
        {
            Messenger.Default.Send(new LoadingEnableMessage {Message = message});
        }
        public void Dispose()
        {
            Messenger.Default.Send(new LoadingDisableMessage());
        }
    }
}
