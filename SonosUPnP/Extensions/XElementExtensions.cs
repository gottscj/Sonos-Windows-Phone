using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Sonos.Models;
using UPnP.Models;

namespace Sonos.Extensions
{
    public static class XElementExtensions
    {
        public static string GetId(this XElement e)
        {
            return e.TryGetAttributeValue("id");
        }

        public static string GetParentId(this XElement e)
        {
            return e.TryGetAttributeValue("parentID");
        }

        public static string GetTitle(this XElement e)
        {
            return e.TryGetElementValue("title", NS.ds);
        }

        public static void SetTitle(this XElement e, string value)
        {
            e.TrySetElementValue("title", NS.ds, value);
        }

        public static string GetClass(this XElement e)
        {
            return e.TryGetElementValue("class", NS.upnp);
        }

        public static string GetArtist(this XElement e)
        {
            return e.TryGetElementValue("creator", NS.ds);
        }

        public static void SetArtist(this XElement e, string value)
        {
            e.TrySetElementValue("creator", NS.ds, value);
        }

        public static string GetDescription(this XElement e)
        {
            return e.TryGetElementValue("description", NS.r);
        }

        public static string GetAlbumUri(this XElement e)
        {
            return e.TryGetElementValue("albumArtURI", NS.upnp);
        }

        public static void SetAlbumArtUri(this XElement e, string value)
        {
            e.TrySetElementValue("albumArtURI", NS.upnp, value);
        }

        public static string GetAlbumTitle(this XElement e)
        {
            return e.TryGetElementValue("album", NS.upnp);
        }

        public static string GetTrackDuration(this XElement e)
        {
            return e.TryGetElementAttributeValue("res", "duration");
        }

        public static string GetTrackUrl(this XElement e)
        {
            return e.TryGetElementValue("res");
        }

        public static string GetTrackNumber(this XElement e)
        {
            return e.TryGetElementValue("originalTrackNumber", NS.upnp);
        }

        public static string GetName(this XElement e)
        {
            return e.TryGetAttributeValue("Name");
        }

        public static string GetVersion(this XElement e)
        {
            return e.TryGetAttributeValue("Version");
        }

        public static string GetUri(this XElement e)
        {
            return e.TryGetAttributeValue("Uri");
        }

        public static string GetSecureUri(this XElement e)
        {
            return e.TryGetAttributeValue("SecureUri");
        }

        public static string GetContainerType(this XElement e)
        {
            return e.TryGetAttributeValue("ContainerType");
        }

        public static string GetCapabilities(this XElement e)
        {
            return e.TryGetAttributeValue("Capabilities");
        }

        public static string GetResource(this XElement e)
        {
            return e.TryGetElementValue("res", NS.didl);
        }

        public static int GetMaxMessagingChars(this XElement e)
        {
            int val;
            int.TryParse(e.TryGetAttributeValue("MaxMessagingChars"), out val);
            return val;
        }

        public static Auth GetAuthType(this XElement e)
        {
            var auth = Auth.Unknown;
            string authTypeStr = e.TryGetElementAttributeValue("Policy", "Auth");
            switch (authTypeStr)
            {
                case "UserId":
                    auth = Auth.UserId;
                    break;
                case "Anonymous":
                    auth = Auth.Anonymous;
                    break;
                case "DeviceLink":
                    auth = Auth.DeviceLink;
                    break;
            }

            return auth;
        }

        public static int GetPollInterval(this XElement e)
        {
            int val;
            int.TryParse(e.TryGetElementAttributeValue("Policy", "PollInterval"), out val);
            return val;
        }

        public static string TryGetElementValue(this XElement e, string name, string xmlns)
        {
            XElement element = e.Element(XName.Get(name, xmlns));

            if (element == null) return string.Empty;

            return element.Value;
        }

        public static void TrySetElementValue(this XElement e, string name, string value)
        {
            XElement element = e.Element(XName.Get(name));

            if (element == null) return;

            element.Value = value;
        }

        public static void TrySetElementValue(this XElement e, string name, string xmlns, string value)
        {
            XElement element = e.Element(XName.Get(name, xmlns));

            if (element == null) return;

            element.Value = value;
        }

        public static string TryGetElementValue(this XElement e, string name)
        {
            XElement element = e.Element(XName.Get(name));

            if (element == null) return string.Empty;

            return element.Value;
        }

        public static string TryGetAttributeValue(this XElement e, string attributeName)
        {
            XAttribute attribute = e.Attribute(XName.Get(attributeName));
            if (attribute == null) return string.Empty;

            return attribute.Value;
        }


        public static string TryGetElementAttributeValue(this XElement e, string name, string xmlns,
            string attributeName)
        {
            XElement element = e.Element(XName.Get(name, xmlns));

            if (element == null) return string.Empty;

            return element.TryGetAttributeValue(attributeName);
        }

        public static string TryGetElementAttributeValue(this XElement e, string name, string attributeName)
        {
            XElement element = e.Element(XName.Get(name));

            if (element == null) return string.Empty;

            return element.TryGetAttributeValue(attributeName);
        }
    }
}