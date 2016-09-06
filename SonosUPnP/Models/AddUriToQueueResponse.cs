using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class AddUriToQueueResponse
    {
        private readonly XElement _xElement;

        public AddUriToQueueResponse(XElement e)
        {
            _xElement = e;
        }

        public string FirstTrackNumberEnqueued
        {
            get { return _xElement.TryGetElementValue("FirstTrackNumberEnqueued"); }
        }

        public string NumTracksAdded
        {
            get { return _xElement.TryGetElementValue("NumTracksAdded"); }
        }

        public string NewQueueLength
        {
            get { return _xElement.TryGetElementValue("NewQueueLength"); }
        }
    }
}