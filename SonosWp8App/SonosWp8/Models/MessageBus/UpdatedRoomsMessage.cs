using System.Collections.Generic;
using System.Windows.Input;
using Sonos.Models;

namespace SonosWp8.Models.MessageBus
{
    public class UpdatedRoomsMessage
    {
        public UpdatedRoomsMessage(List<ZoneMember> zoneMembers, ICommand command)
        {
            ZoneMembers = zoneMembers;
            Command = command;
        }

        public List<ZoneMember> ZoneMembers { get; set; }
        public ICommand Command { get; set; }
    }
}