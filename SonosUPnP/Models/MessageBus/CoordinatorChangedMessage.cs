using Sonos.Devices;

namespace Sonos.Models.MessageBus
{
    public class CoordinatorChangedMessage
    {
        public ZonePlayer Coordinator { get; set; }
    }
}
