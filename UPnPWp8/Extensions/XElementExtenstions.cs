using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace UPnP.Extensions
{
    public static class XElementExtenstions
    {
        public static string TryGetValue(this XElement e, string name)
        {
            var value = string.Empty;

            var element = e
                .Descendants(XName.Get(name, "urn:schemas-upnp-org:device-1-0"))
                .FirstOrDefault();
            if (element != null)
            {
                value = element.Value;
            }

            return value;
        }

        public static XElement TryGetElement(this XElement e, string name)
        {
            return e
                .Descendants(XName.Get(name, "urn:schemas-upnp-org:device-1-0"))
                .FirstOrDefault();
        }

        public static IEnumerable<XElement> GetDescendants(this XElement e, string name)
        {
            return e.Descendants(XName.Get(name, "urn:schemas-upnp-org:device-1-0"));
        }
    }
}