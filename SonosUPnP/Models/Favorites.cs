using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class Favorites : ContainerWithImage
    {
        //<item id="FV:2/0" parentID="FV:2" restricted="false">
        //  <dc:title>Get Free</dc:title>
        //  <upnp:class>object.itemobject.item.sonos-favorite</upnp:class>
        //  <r:ordinal>1</r:ordinal>
        //  <res protocolInfo="sonos.com-http:*:audio/mp4:*">x-sonos-http:trackid_15187458.mp4?sid=20&amp;flags=32</res>
        //  <upnp:albumArtURI>/getaa?s=1&amp;u=x-sonos-http%3atrackid_15187458.mp4%3fsid%3d20%26flags%3d32</upnp:albumArtURI>
        //  <r:type>instantPlay</r:type>
        //  <r:description>By Major Lazer</r:description>
        //  <r:resMD>&lt;DIDL-Lite xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:upnp="urn:schemas-upnp-org:metadata-1-0/upnp/" xmlns:r="urn:schemas-rinconnetworks-com:metadata-1-0/" xmlns="urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/"&gt;&lt;item id="00030000trackid_15187458" parentID="" restricted="true"&gt;&lt;dc:title&gt;Get Free&lt;/dc:title&gt;&lt;upnp:class&gt;object.item.audioItem.musicTrack&lt;/upnp:class&gt;&lt;desc id="cdudn" nameSpace="urn:schemas-rinconnetworks-com:metadata-1-0/"&gt;SA_RINCON5127_25303079&lt;/desc&gt;&lt;/item&gt;&lt;/DIDL-Lite&gt;</r:resMD>
        //</item>
        private readonly string _imageBaseAddress = string.Empty;

        public Favorites(XElement e, string address)
            : base(e)
        {
            _imageBaseAddress = address;
        }

        public override string ImageUri
        {
            get
            {
                string uri = XElement.GetAlbumUri();
                if (!uri.StartsWith("http"))
                {
                    uri = _imageBaseAddress + uri;
                }
                return uri;
            }
        }

        public string Artist
        {
            get { return XElement.GetDescription(); }
        }
    }
}