using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class Track : ContainerWithImage
    {
        private readonly string _address;

        public Track()
        {
        }

        public Track(XElement e, string address)
            : base(e)
        {
            _address = address;
        }

        //Each element looks like this:
        //  <item id="S://10.0.0.2/Music/Fun%20Lovin'%20Criminals%20-%20Discography%201995-2005/100%25%20Colombian/01%20-%20Up%20On%20The%20Hill.mp3" parentID="A:ALBUM/100%25%20Colombian" restricted="true" xmlns="urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/">
        //  <res protocolInfo="x-file-cifs:*:audio/mpeg:*">x-file-cifs://10.0.0.2/Music/Fun%20Lovin'%20Criminals%20-%20Discography%201995-2005/100%25%20Colombian/01%20-%20Up%20On%20The%20Hill.mp3</res>
        //  <albumArtURI xmlns="urn:schemas-upnp-org:metadata-1-0/upnp/">/getaa?u=x-file-cifs%3a%2f%2f10.0.0.2%2fMusic%2fFun%2520Lovin'%2520Criminals%2520-%2520Discography%25201995-2005%2f100%2525%2520Colombian%2f01%2520-%2520Up%2520On%2520The%2520Hill.mp3&amp;v=5</albumArtURI>
        //  <title xmlns="http://purl.org/dc/elements/1.1/">Up On The Hill</title>
        //  <class xmlns="urn:schemas-upnp-org:metadata-1-0/upnp/">object.item.audioItem.musicTrack</class>
        //  <creator xmlns="http://purl.org/dc/elements/1.1/">Fun Lovin' Criminals</creator>
        //  <album xmlns="urn:schemas-upnp-org:metadata-1-0/upnp/">100% Colombian</album>
        //  <originalTrackNumber xmlns="urn:schemas-upnp-org:metadata-1-0/upnp/">1</originalTrackNumber>
        //  </item>
        public string TrackNumber
        {
            get { return XElement.GetTrackNumber(); }
        }

        public override string ImageUri
        {
            get
            {
                string albumArtUri = XElement.GetAlbumUri();
                if (albumArtUri == string.Empty) return string.Empty;

                return _address + albumArtUri;
            }
        }

        public string Artist
        {
            get { return XElement.GetArtist(); }
        }

        public string AlbumTitle
        {
            get { return XElement.GetAlbumTitle(); }
        }

        public string Duration
        {
            get { return XElement.GetTrackDuration(); }
        }

        public string Url
        {
            get { return XElement.GetTrackUrl(); }
        }
    }
}