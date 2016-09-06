using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.GZip;
using Microsoft.Phone.Net.NetworkInformation;
using UPnP.Extensions;
using UPnP.Models;

namespace UPnP
{
    public class Service
    {
        private readonly XElement _description;
        private string _eventUuid = string.Empty;
        private string _name = string.Empty;

        public Service(Service service) :
            this(service.Description, service.BaseAddress)
        {
        }

        internal Service(XElement description, string baseAddress)
        {
            _description = description;
            BaseAddress = baseAddress;
        }

        protected XElement Description
        {
            get { return _description; }
        }

        protected string BaseAddress { get; set; }

        public string ServiceType
        {
            get { return Description.TryGetValue("serviceType"); }
        }

        public string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(_name)) return _name;
                var split = ServiceId.Split(':');
                _name = split.Length > 0 ? split.Last() : ServiceId;

                return _name;
            }
        }

        public string ServiceId
        {
            get { return Description.TryGetValue("serviceId"); }
        }

        public string ControlUrl
        {
            get { return BaseAddress + Description.TryGetValue("controlURL"); }
        }

        public string EventSubUrl
        {
            get { return BaseAddress + Description.TryGetValue("eventSubURL"); }
        }

        public string ScpdUrl
        {
            get { return BaseAddress + Description.TryGetValue("SCPDURL"); }
        }

        public string EventUuid
        {
            get { return _eventUuid; }
        }

        public void SetEventUuid(string id)
        {
            _eventUuid = id;
        }

        protected Task<SoapActionResult> InvokeActionAsync(SoapAction action, object[] args)
        {
            return InvokeActionAsync(action, args, "", true);
        }

        protected Task<SoapActionResult> InvokeActionAsync(SoapAction action, object[] args, string header,
            bool addXmlNs)
        {
            return InvokeActionAsync(action, args, header, addXmlNs, "");
        }

        protected async Task<SoapActionResult> InvokeActionAsync(SoapAction action, object[] args, string header,
            bool addXmlNs, string accessToken)
        {
            SoapActionResult result;
            try
            {
                var rootElementStart = "<" + action.Name + " xmlns=\"" + ServiceType + "\">";
                var rootElementEnd = "</" + action.Name + ">";
                const string format = "<{0}>{1}</{0}>";

                if (addXmlNs)
                {
                    rootElementStart = "<u:" + action.Name + " xmlns:u=\"" + ServiceType + "\">";
                    rootElementEnd = "</u:" + action.Name + ">";
                }

                var soap = new StringBuilder(rootElementStart);

                if (args != null)
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        soap.AppendFormat(format, action.ArgNames[i], FormatArg(args[i]));
                    }
                }

                soap.AppendFormat(rootElementEnd);

                result = await SoapRequestAsync(soap.ToString(), action.Name, header).ConfigureAwait(false);

                // just return in case of error
                if (result.Error != null) return new SoapActionResult(result.Error, result.XElement);

                if (action.ExpectedReplyCount != 0)
                {
                    // Children of u:<VERB>Response are the result we want
                    var xn = XName.Get(action.Name + "Response", ServiceType);
                    var responseElement = result.XElement.Descendants(xn).FirstOrDefault();
                    result.XElement = responseElement;
                }
            }
            catch (Exception ex)
            {
                result = new SoapActionResult(new SoapActionError(ex, action.Name), new XElement("ERROR"));
            }

            return result;
        }

        private async Task<SoapActionResult> SoapRequestAsync(string soap, string action, string header)
        {
            //string req = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            //var req = "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
            var req = "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\""+ 
                       " s:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">"+
                         header +
                         "<s:Body>" +
                         soap +
                         "</s:Body>" +
                         "</s:Envelope>";
            var uri = new Uri(ControlUrl);

            var r = (HttpWebRequest) WebRequest.Create(uri);
#if false
            Debug.WriteLine(req);
#endif
            // We only ever need to do this over non-cellular networks
            r.SetNetworkPreference(NetworkSelectionCharacteristics.NonCellular);
            r.Method = "POST";
            byte[] b = Encoding.UTF8.GetBytes(req);
            r.Headers["SOAPACTION"] = "\"" + ServiceType + "#" + action + "\"";
            r.ContentType = "text/xml; charset=\"utf-8\"";

            using (var stream = await r.GetRequestStreamAsync())
            {
                stream.Write(b, 0, b.Length);
            }

            WebResponse resp = null;
            Exception exception = null;
            try
            {
                resp = await r.GetResponseAsync();
            }
            catch (WebException ex)
            {
                exception = ConvertException(ex);
            }

            SoapActionError error = null;
            if (exception != null)
            {
                error = new SoapActionError(exception, req);
                Debug.WriteLine("error: " + error.Exception.Message);
            }

            var result = new SoapActionResult(error, null);
            if (result.Error == null && resp != null)
            {
                Stream stream = null;
                try
                {
                    stream = resp.GetResponseStream();
                    stream.Seek(0, SeekOrigin.Begin);
                    if (resp.Headers["Content-Encoding"] == "gzip")
                    {
                        stream = new GZipInputStream(stream);
                    }
                    result.XElement = XElement.Load(stream);
                }
                catch (Exception e)
                {
                    result.XElement = new XElement("ERROR");
                    if (result.Error != null)
                    {
                        result.Error.Exception = e;
                        result.Error.Request = req;
                    }
                }
                finally
                {
                    if (stream != null) stream.Dispose();
                }
            }
            else
            {
                result.XElement = new XElement("ERROR");
            }
            if (resp != null)
            {
                resp.Dispose();
            }

            return result;
        }

        // If an exception is really a UPNP error, convert it
        private static Exception ConvertException(WebException ex)
        {
            var resp = ex.Response;

            if (resp == null) return ex;
            using (var respstream = resp.GetResponseStream())
            {
                respstream.Seek(0, SeekOrigin.Begin);
                TextReader reader = new StreamReader(respstream, Encoding.UTF8);
                var error = reader.ReadToEnd();

                // This can be a UPnP error
                // TODO actually parse the XML
                var errstart = error.IndexOf("<errorCode>", StringComparison.Ordinal);
                var errend = error.IndexOf("</errorCode>", StringComparison.Ordinal);
                if ((errstart != -1) && (errend != -1))
                {
                    var errorNumber = error.Substring(errstart + 11, errend - errstart - 11);
                    int number;
                    if (!int.TryParse(errorNumber, out number)) return ex;

                    return new UPnPException(number);
                }
                errstart = error.IndexOf("<ExceptionInfo>", StringComparison.Ordinal);
                errend = error.IndexOf("</ExceptionInfo>", StringComparison.Ordinal);
                if ((errstart == -1) || (errend == -1)) return ex;
                var errorString = error.Substring(errstart + 15, errend - errstart - 15);
                return new Exception(errorString);
            }
        }

        private static string FormatArg(object obj)
        {
            string result = string.Empty;

            if (obj == null)
                result = "";
            else if (obj is string)
                result = Utility.XmlEscape(obj.ToString());
            else if (obj is bool)
                result = (bool) (obj) ? "1" : "0";
            else
            {
                var changeType = Convert.ChangeType(obj, obj.GetType(), CultureInfo.InvariantCulture);
                if (changeType != null)
                    result = changeType.ToString();
            }

            return result;
        }

        protected void SubcribeToEvents()
        {
            GenaService.Default.Subscribe(this, OnStateVariableChanged);
        }

        protected void UnsubscribeToEvents()
        {
            GenaService.Default.Unsubscribe(this);
        }

        /// <summary>
        ///     Raises the StateVariableChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnStateVariableChanged(StateVariableChangedEventArgs e)
        {
        }
    }
}