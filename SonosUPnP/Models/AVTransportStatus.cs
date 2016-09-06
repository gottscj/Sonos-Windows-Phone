using System.Linq;
using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class AvTransportStatus
    {
        public readonly XElement XElement;

        public AvTransportStatus(XContainer element)
        {
            XElement =
                element.Descendants(XName.Get("InstanceID", "urn:schemas-upnp-org:metadata-1-0/AVT/")).FirstOrDefault();
        }

        public TransportState TransportState
        {
            get
            {
                string val = XElement.TryGetElementAttributeValue("TransportState",
                    "urn:schemas-upnp-org:metadata-1-0/AVT/", "val");
                if (string.IsNullOrEmpty(val))
                {
                    return TransportState.Pausedplayback;
                }
                return AvTransport.ParseTransportState(val);
            }
        }

        public string CurrentTrackUri
        {
            get
            {
                return XElement.TryGetElementAttributeValue("CurrentTrackURI", "urn:schemas-upnp-org:metadata-1-0/AVT/",
                    "val");
            }
        }

        public string AvTransportUri
        {
            get
            {
                return XElement.TryGetElementAttributeValue("AVTransportURI", "urn:schemas-upnp-org:metadata-1-0/AVT/",
                    "val");
            }
        }
    }
}