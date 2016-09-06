using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class TransportInfo
    {
        private readonly XElement _xElement;

        public TransportInfo(XElement element)
        {
            _xElement = element;
        }

        public TransportState TransportState
        {
            get
            {
                var val = _xElement.TryGetElementValue("CurrentTransportState");
                return string.IsNullOrEmpty(val) ? 
                    TransportState.Pausedplayback : 
                    AvTransport.ParseTransportState(val);
            }
        }

        public static TransportInfo Create(TransportState transportState)
        {
            var element = "<u:GetTransportInfoResponse xmlns:u=\"urn:schemas-upnp-org:service:AVTransport:1\">" +
                             "<CurrentTransportState>{0}</CurrentTransportState>" +
                             "<CurrentTransportStatus>OK</CurrentTransportStatus>" +
                             "<CurrentSpeed>1</CurrentSpeed>" +
                             "</u:GetTransportInfoResponse>";
            element = string.Format(element, transportState);

            return new TransportInfo(XElement.Parse(element));
        }
    }
}