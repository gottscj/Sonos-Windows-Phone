using System.Xml.Linq;
using Sonos.Models;

namespace Sonos
{
    public static class ContainerFactory
    {
        /// <summary>
        ///     Create base upnp container
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <param name="title"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        public static Container Create(string id, string parentId, string title, string classId)
        {
            var xml = string.Format(
                "<container id=\"{0}\" parentID=\"{1}\" restricted=\"true\" xmlns=\"urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/\">" +
                "<dc:title xmlns:dc=\"http://purl.org/dc/elements/1.1/\">{2}</dc:title>" +
                "<upnp:class xmlns:upnp=\"urn:schemas-upnp-org:metadata-1-0/upnp/\">{3}</upnp:class>" +
                "</container>", id, parentId, title, classId);
            var element = XElement.Parse(xml);
            return new Container(element);
        }

        /// <summary>
        ///     Create base upnp container
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <param name="title"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        public static T Create<T>(string id, string parentId, string title, string classId) where T : Container
        {
            var xml = string.Format(
                "<container id=\"{0}\" parentID=\"{1}\" restricted=\"true\" xmlns=\"urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/\">" +
                "<dc:title xmlns:dc=\"http://purl.org/dc/elements/1.1/\">{2}</dc:title>" +
                "<upnp:class xmlns:upnp=\"urn:schemas-upnp-org:metadata-1-0/upnp/\">{3}</upnp:class>" +
                "</container>", id, parentId, title, classId);
            XElement element = XElement.Parse(xml);
            return (T)new Container(element);
        }

        /// <summary>
        ///     Creates a upnp container with imageuri
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <param name="title"></param>
        /// <param name="classId"></param>
        /// <param name="imageUri"></param>
        /// <returns></returns>
        public static ContainerWithImage Create(string id, string parentId, string title, string classId, string imageUri)
        {
            string xml = string.Format(
                "<container id=\"{0}\" parentID=\"{1}\" restricted=\"true\" xmlns=\"urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/\">" +
                "<dc:title xmlns:dc=\"http://purl.org/dc/elements/1.1/\">{2}</dc:title>" +
                "<upnp:class xmlns:upnp=\"urn:schemas-upnp-org:metadata-1-0/upnp/\">{3}</upnp:class>" +
                "<upnp:albumArtURI xmlns:upnp=\"urn:schemas-upnp-org:metadata-1-0/upnp/\">{4}</upnp:albumArtURI>" +
                "</container>", id, parentId, title, classId, imageUri);

            XElement element = XElement.Parse(xml);
            return new ContainerWithImage(element);
        }

        /// <summary>
        ///     Create container from xelement.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Container Create(XElement e)
        {
            //switch (model.ClassId)
            //{
            // TODO: create by class id
            //}
            if (e.Name.LocalName == "Service")
            {
                return new MusicServiceContainer(e);
            }

            return new Container(e);
        }

        public static T Create<T>(XElement e) where T : Container
        {
            var container = Create(e) as T;
            return container;
        }

        public static T Create<T>(Container container) where T : Container
        {
            return Create<T>(container.XElement);
        }
    }
}