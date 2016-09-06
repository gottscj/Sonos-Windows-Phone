using System;
using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class ZoneVolume
    {
        private readonly XElement _xElement;
        private readonly ZoneMute _zoneMute;

        public ZoneVolume(XElement e, ZoneMute zoneMute)
        {
            _xElement = e;
            _zoneMute = zoneMute;
        }

        public int Volume
        {
            get
            {
                if (_xElement == null) return 0;

                string volStr = _xElement.TryGetElementValue("CurrentVolume");
                if (string.IsNullOrEmpty(volStr)) return 0;

                return Convert.ToInt32(volStr);
            }
        }

        public bool IsMuted
        {
            get { return _zoneMute.IsMuted; }
        }
    }
}