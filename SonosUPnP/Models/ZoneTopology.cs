using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class ZoneTopology
    {
        public ZoneTopology(List<ZoneMember> members)
        {
            Members = members;
        }

        public List<ZoneMember> Members { get; private set; }
    }
}