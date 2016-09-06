using System.Xml.Linq;
using Sonos.Extensions;

namespace Sonos.Models
{
    public class Container
    {
        public Container()
        {
        }

        public Container(XElement e)
        {
            XElement = e;
        }

        public XElement XElement { get; private set; }

        public virtual string Id
        {
            get { return XElement.GetId(); }
        }

        public virtual string ParentId
        {
            get { return XElement.GetParentId(); }
        }

        public virtual string Title
        {
            get { return XElement.GetTitle(); }
        }

        //public bool Restricted { get; set; }
        public virtual string ClassId
        {
            get { return XElement.GetClass(); }
        }

        public virtual string Resource
        {
            get { return XElement.GetResource(); }
        }

        public override string ToString()
        {
            return string.Format("Id: {0}, Title: {1}, ClassId: {2}", Id, Title, ClassId);
        }
    }
}