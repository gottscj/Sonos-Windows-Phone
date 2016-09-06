using System;
using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class MusicServiceContainer : ContainerWithImage
    {
        internal MusicServiceContainer(XElement e) : base(e)
        {
        }

        public MusicServiceContainer(XElement e, string imageUri, string serviceId, string token) : this(e)
        {
            e.Add(new XAttribute("ImageUri", imageUri));
            e.Add(new XAttribute("ServiceId", serviceId));
            e.Add(new XAttribute("Token", token));
        }

        public override string Id
        {
            get { return XElement.TryGetAttributeValue("Id"); }
        }

        public override string ParentId
        {
            get { return string.Empty; }
        }

        public override string Title
        {
            get { return XElement.GetName(); }
        }

        public override string ClassId
        {
            get { return XElement.GetContainerType(); }
        }

        public string Artist
        {
            get { return XElement.TryGetAttributeValue("Artist"); }
        }

        public string Token
        {
            get { return XElement.TryGetAttributeValue("Token"); }
        }

        public Auth Auth
        {
            get { return XElement.GetAuthType(); }
        }

        public int PollInterval
        {
            get { return XElement.GetPollInterval(); }
        }

        public string Version
        {
            get { return XElement.GetVersion(); }
        }

        public string Uri
        {
            get { return XElement.GetUri(); }
        }

        public string SecureUri
        {
            get { return XElement.GetSecureUri(); }
        }

        public override string ImageUri
        {
            get { return XElement.TryGetAttributeValue("ImageUri"); }
        }

        public string ServiceId
        {
            get { return XElement.TryGetAttributeValue("ServiceId"); }
        }

        public void SetId(string id)
        {
            XElement.SetAttributeValue(XName.Get("Id"), id);
        }

        public void SetClassId(string classId)
        {
            XElement.SetAttributeValue(XName.Get("ContainerType"), classId);
        }

        public void SetToken(string id)
        {
            XElement.SetAttributeValue(XName.Get("Token"), id);
        }

        public void SetImageUri(string uri)
        {
            XElement.SetAttributeValue(XName.Get("ImageUri"), uri);
        }

        public void SetTitle(String title)
        {
            XElement.SetAttributeValue(XName.Get("Name"), title);
        }
    }
}