using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class Album : ContainerWithImage
    {
        //  <container id="A:ALBUM/100%25%20Colombian" parentID="A:ALBUM" restricted="true" xmlns="urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/">
        //  <dc:title xmlns:dc="http://purl.org/dc/elements/1.1/">100% Colombian</dc:title>
        //  <upnp:class xmlns:upnp="urn:schemas-upnp-org:metadata-1-0/upnp/">object.container.album.musicAlbum</upnp:class>
        //  <res protocolInfo="x-rincon-playlist:*:*:*">x-rincon-playlist:RINCON_000E5874AA8601400#A:ALBUM/100%25%20Colombian</res>
        //  <dc:creator xmlns:dc="http://purl.org/dc/elements/1.1/">Fun Lovin' Criminals</dc:creator>
        //  <upnp:albumArtURI xmlns:upnp="urn:schemas-upnp-org:metadata-1-0/upnp/">/getaa?u=x-file-cifs%3a%2f%2f10.0.0.2%2fMusic%2fFun%2520Lovin'%2520Criminals%2520-%2520Discography%25201995-2005%2f100%2525%2520Colombian%2f01%2520-%2520Up%2520On%2520The%2520Hill.mp3&amp;v=5</upnp:albumArtURI>
        //  </container>
        private readonly string _address;

        public Album()
        {
        }

        public Album(XElement e, string address)
            : base(e)
        {
            _address = address;
        }

        public override string ImageUri
        {
            get
            {
                string albumArtUri = XElement.GetAlbumUri();
                if (albumArtUri == "") return "";

                return _address + albumArtUri;
            }
        }

        public string Artist
        {
            get { return XElement.GetArtist(); }
        }
    }
}