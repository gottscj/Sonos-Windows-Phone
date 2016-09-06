using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class ContainerWithImage : Container
    {
        private string _imageUri;

        public ContainerWithImage()
        {
        }

        public ContainerWithImage(XElement e)
            : base(e)
        {
            _imageUri = XElement.GetAlbumUri();
        }

        public virtual string ImageUri
        {
            get { return _imageUri; }
            set { _imageUri = value; }
        }
    }
}