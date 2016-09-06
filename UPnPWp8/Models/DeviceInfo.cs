using System;
using System.Xml.Linq;

namespace UPnP.Models
{
    public class DeviceInfo
    {
        public DeviceInfo()
        {
            DescriptionUri = null;
            XElement = null;
        }

        public DeviceInfo(Uri descriptionUri, XElement xElement)
        {
            DescriptionUri = descriptionUri;
            BaseAddress = "http://" + descriptionUri.Authority;
            XElement = xElement;
        }

        public Uri DescriptionUri { get; set; }
        public string BaseAddress { get; set; }
        public XElement XElement { get; set; }
    }
}