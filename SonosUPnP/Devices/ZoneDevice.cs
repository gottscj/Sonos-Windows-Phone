using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sonos.Models;
using UPnP;
using UPnP.Extensions;

namespace Sonos.Devices
{
    public class ZoneDevice : Device
    {
        private readonly string _displayName = string.Empty;
        private readonly string _zoneName = string.Empty;
        private readonly ZoneType _zoneType = ZoneType.Unknown;

        public ZoneDevice(Device device)
            : base(device)
        {
            _zoneName = Description.TryGetValue("roomName");
            _displayName = Description.TryGetValue("displayName");
            var tmpZoneType = Description.TryGetValue("zoneType");
            switch (tmpZoneType)
            {
                    // TODO add all zoneTypes here
                case "4":
                    _zoneType = ZoneType.Bridge;
                    break;
                case "7":
                    _zoneType = ZoneType.Play3;
                    break;
            }
        }

        public string ZoneName
        {
            get { return _zoneName; }
        }

        public string DisplayName
        {
            get { return _displayName; }
        }

        public ZoneType ZoneType
        {
            get { return _zoneType; }
        }

        public static async Task<List<ZoneDevice>> FindAndCreateAsync()
        {
            var scanner = new Scanner();
            var devices = await scanner.FindDevices(DeviceTypes.ZonePlayer, 2);

            return devices.Select(device => new ZoneDevice(device)).ToList();
        }

        public static async Task<List<ZoneDevice>> FindAndCreateAsync(bool ignoreBridge)
        {
            List<ZoneDevice> zones = await FindAndCreateAsync();

            if (ignoreBridge)
                return zones.Where(x => x.ZoneType != ZoneType.Bridge).ToList();
            return zones.ToList();
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}