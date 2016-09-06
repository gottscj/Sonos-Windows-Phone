using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class ZoneMember
    {
        private readonly XElement _myElement;

        public ZoneMember()
        {
        }

        public ZoneMember(XElement e)
        {
            _myElement = e;
        }

        public string Title
        {
            get { return _myElement.Value; }
        }

        public string Uuid
        {
            get { return _myElement.TryGetAttributeValue("uuid"); }
        }

        public string Location
        {
            get { return _myElement.TryGetAttributeValue("location"); }
        }

        public bool IsCoordinator
        {
            get { return _myElement.TryGetAttributeValue("coordinator") == "true"; }
        }
    }
}