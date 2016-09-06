using System;
using System.Linq;
using System.Xml.Linq;

namespace Sonos.Models
{
    public class ActionResult
    {
        private readonly XElement _actionResult;

        private int _numberReturned = -1;
        private XElement _result;

        private int _totalMatches = -1;

        public ActionResult(XElement e)
        {
            Exception = null;
            _actionResult = e;
        }

        public ActionResult(XElement e, Exception exception) : this(e)
        {
            Exception = exception;
        }

        public Exception Exception { get; set; }

        public XElement Result
        {
            get
            {
                if (_result != null) return _result;

                XElement tmp = _actionResult
                    .Descendants(XName.Get("Result"))
                    .FirstOrDefault();
                _result = tmp != null ? 
                    XElement.Parse(tmp.Value) : 
                    _actionResult;

                return _result;
            }
        }

        public int NumberReturned
        {
            get
            {
                if (_numberReturned != -1) return _numberReturned;

                XElement node = _actionResult.Element(XName.Get("NumberReturned"));
                if (node != null)
                {
                    int.TryParse(node.Value, out _numberReturned);
                }
                else
                {
                    node = _actionResult
                        .Descendants(XName.Get("count", ServiceTypes.SonosServices))
                        .FirstOrDefault();
                    if (node != null)
                    {
                        int.TryParse(node.Value, out _numberReturned);
                    }
                }

                return _numberReturned;
            }
        }

        public int TotalMatches
        {
            get
            {
                if (_totalMatches != -1) return _totalMatches;

                XElement node = _actionResult.Element(XName.Get("TotalMatches"));
                if (node != null)
                {
                    int.TryParse(node.Value, out _totalMatches);
                }
                else
                {
                    node = _actionResult
                        .Descendants(XName.Get("total", ServiceTypes.SonosServices))
                        .FirstOrDefault();
                    if (node != null)
                    {
                        int.TryParse(node.Value, out _totalMatches);
                    }
                }

                return _totalMatches;
            }
        }
    }
}