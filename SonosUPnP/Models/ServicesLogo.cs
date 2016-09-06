using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class ServicesLogo
    {
        private static readonly string[] PlacementTypes = {"square:medium", "square", "large"};
        private readonly XElement _element;
        private string _id = string.Empty;
        private string _imageUri = "";

        private string _name = "";

        public ServicesLogo(XElement e)
        {
            _element = e;
        }

        public string Id
        {
            get
            {
                if (_id != string.Empty) return _id;

                _id = _element.TryGetAttributeValue("id");
                return _id;
            }
        }

        public string Name
        {
            get
            {
                if (_name != "") return _name;

                XNode comment =
                    _element.DescendantNodes().Where(x => x.NodeType == XmlNodeType.Comment).FirstOrDefault();
                if (comment != null)
                {
                    _name = comment.ToString().Replace("<!--", "").Replace("-->", "").Trim();
                }
                return _name;
            }
        }

        public string ImageUri
        {
            get
            {
                if (_imageUri != "") return _imageUri;

                foreach (var image in PlacementTypes.Select(type => _element.Elements()
                    .FirstOrDefault(x => x.TryGetAttributeValue("placement") == type)).Where(image => image != null))
                {
                    _imageUri = image.Value.Replace("\"n", "").Trim();
                    if (_imageUri.EndsWith("svg"))
                        _imageUri = string.Empty;
                    else
                        break;
                }


                return _imageUri;
            }
        }

        public override string ToString()
        {
            return _element.ToString();
            //return Name + ": " + ImageUri;
        }
    }
}