using System;
using System.Linq;
using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class PositionInfo
    {
        private readonly string _baseAddress;
        private readonly XElement _element;
        private Track _trackMetaData;

        public PositionInfo(XElement element, string address)
        {
            _baseAddress = address;
            _element = element;
        }

        public int QueueIndex
        {
            get
            {
                string idx = _element.TryGetElementValue("Track");
                if (idx != "") return Convert.ToInt32(idx);
                return 0;
            }
        }

        public string TrackDuration
        {
            get { return _element.TryGetElementValue("TrackDuration"); }
        }

        public Track Track
        {
            get
            {
                if (_trackMetaData != null) return _trackMetaData;
                var trackMetaDataString = _element.TryGetElementValue("TrackMetaData");
                if (trackMetaDataString == "" || trackMetaDataString == "NOT_IMPLEMENTED") 
                    return _trackMetaData;
                
                var trackMetaDataElement = XElement.Parse(trackMetaDataString);
                
                trackMetaDataElement = trackMetaDataElement.Descendants().First();
                _trackMetaData = new Track(trackMetaDataElement, _baseAddress);

                return _trackMetaData;
            }
        }

        public string RelTime
        {
            get { return _element.TryGetElementValue("RelTime"); }
        }
    }
}