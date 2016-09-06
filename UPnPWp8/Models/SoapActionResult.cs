using System;
using System.Xml.Linq;

namespace UPnP.Models
{
    public class SoapActionResult
    {

        public SoapActionResult(SoapActionError error, XElement xElement)
        {
            Error = error;
            XElement = xElement;
        }

        public SoapActionError Error { get; set; }
        public XElement XElement { get; set; }
    }

    public class SoapActionError
    {
        public SoapActionError() : this(null, "")
        {
        }

        public SoapActionError(Exception exception, string request)
        {
            Exception = exception;
            Request = request;
        }

        public Exception Exception { get; set; }
        public string Request { get; set; }
    }
}