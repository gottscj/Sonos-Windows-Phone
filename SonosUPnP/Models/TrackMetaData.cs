using System.Xml.Linq;

namespace Sonos.Models
{
    public class TrackMetaData
    {
        private readonly XElement _element;

        public TrackMetaData(XElement element)
        {
            _element = element;
        }

        public string ProtocolInfo { get; set; }

        public XElement Element
        {
            get { return _element; }
        }
    }
}