using System.Collections.Generic;

namespace Sonos.Models
{
    public class ZoneInfo
    {
        public ZoneInfo()
        {
        }

        public ZoneInfo(IReadOnlyList<string> list)
        {
            SerialNumber = list[0];
            SoftwareVersion = list[1];
            DisplaySoftwareVersion = list[2];
            HardwareVersion = list[3];
            IpAddress = list[4];
            MacAddress = list[5];
            CopyrightInfo = list[6];
            ExtraInfo = list[7];
        }

        public string SerialNumber { get; set; }
        public string SoftwareVersion { get; set; }
        public string DisplaySoftwareVersion { get; set; }
        public string HardwareVersion { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public string CopyrightInfo { get; set; }
        public string ExtraInfo { get; set; }
    }
}