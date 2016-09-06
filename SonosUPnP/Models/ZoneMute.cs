using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class ZoneMute
    {
        private readonly XElement _xElement;

        public ZoneMute(XElement xElement)
        {
            _xElement = xElement;
        }

        public bool IsMuted
        {
            get
            {
                if (_xElement == null) return false;

                string boolStr = _xElement.TryGetElementValue("CurrentMute");
                if (string.IsNullOrEmpty(boolStr)) return false;

                return boolStr == "1";
            }
        }
    }
}