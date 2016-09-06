using System.Linq;
using System.Xml.Linq;

namespace Sonos.Models
{
    public class DeviceLink
    {
        private readonly XElement _myElement;

        public DeviceLink(XElement element)
        {
            _myElement = element;
        }

        public string Url
        {
            get
            {
                string regUrl = "";
                if (_myElement == null) return "";
                XElement regUrlNode = _myElement
                    .Descendants(XName.Get("regUrl", ServiceTypes.SonosServices))
                    .FirstOrDefault();

                if (regUrlNode != null) regUrl = regUrlNode.Value;
                return regUrl;
            }
        }

        public string LinkCode
        {
            get
            {
                string linkCode = "";
                if (_myElement == null) return "";
                XElement linkCodeNode = _myElement
                    .Descendants(XName.Get("linkCode", ServiceTypes.SonosServices))
                    .FirstOrDefault();

                if (linkCodeNode != null) linkCode = linkCodeNode.Value;
                return linkCode;
            }
        }
    }
}