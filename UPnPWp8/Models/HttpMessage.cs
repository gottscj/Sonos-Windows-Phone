using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace UPnP.Models
{
    public class HttpMessage
    {
        private Dictionary<string, string> _headers;
        private string _method = string.Empty;
        private string _rawHttp = "";

        private string _serviceName = string.Empty;

        public HttpMessage(Stream stream)
        {
            string rawHttp;
            using (var reader = new StreamReader(stream))
            {
                rawHttp = reader.ReadToEnd();
            }

            FillHeader(rawHttp);
        }

        public HttpMessage(byte[] buffer)
        {
            string rawHttp = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            FillHeader(rawHttp);
        }

        public HttpMessage(string rawHttp)
        {
            FillHeader(rawHttp);
        }

        public string ServiceName
        {
            get { return _serviceName; }
            private set { _serviceName = value; }
        }

        public string Method
        {
            get { return _method; }
            private set { _method = value; }
        }

        public Dictionary<string, string> Headers
        {
            get { return _headers ?? (_headers = new Dictionary<string, string>()); }
        }

        public string Body { get; set; }

        private void FillHeader(string rawHttp)
        {
            _rawHttp = rawHttp;
            var headersBody = rawHttp.Split(new[] {"\r\n\r\n"}, StringSplitOptions.None);

            if (headersBody.Length != 2)
            {
                throw new ArgumentException("Cannot read message: " + rawHttp);
            }
            var headers = headersBody[0].Split(new[] {"\r\n"}, StringSplitOptions.None);
            var body = headersBody[1];

            var j = headers[0].IndexOf(' ');
            Method = headers[0].Substring(0, j);
            var k = headers[0].LastIndexOf(' ');

            ServiceName = headers[0].Substring(j, k - j).Trim();
            if (ServiceName.StartsWith("/"))
            {
                ServiceName = ServiceName.Substring(1);
            }
            for (var i = 1; i < headers.Length; i++)
            {
                var header = headers[i];
                j = header.IndexOf(':');
                var key = header.Substring(0, j).Trim();
                var value = header.Substring(j + 1).Trim(); // +1 to get past ":"
                Headers.Add(key, value);
            }
            Body = HttpUtility.HtmlDecode(body);
        }

        public override string ToString()
        {
            return _rawHttp;
        }
    }
}