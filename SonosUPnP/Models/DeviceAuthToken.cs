using System.Linq;
using System.Xml.Linq;

namespace Sonos.Models
{
    public class DeviceAuthToken
    {
        private readonly XElement _myElement;
        private string _key = "";

        private string _token = "";

        public DeviceAuthToken(XElement element)
        {
            _myElement = element;
        }

        public XElement XElement
        {
            get { return _myElement; }
        }

        public string Token
        {
            get
            {
                if (_token != "") return _token;

                if (_myElement == null) return "";
                XElement authElement =
                    _myElement.Descendants(XName.Get("authToken", ServiceTypes.SonosServices))
                        .FirstOrDefault();
                if (authElement == null) return "";
                _token = authElement.Value;
                return _token;
            }
        }

        public string Key
        {
            get
            {
                if (_key != "") return _key;

                if (_myElement == null) return "";
                XElement keyElement =
                    _myElement.Descendants(XName.Get("privateKey", ServiceTypes.SonosServices))
                        .FirstOrDefault();
                if (keyElement == null) return "";
                _key = keyElement.Value;
                return _key;
            }
        }
    }
}