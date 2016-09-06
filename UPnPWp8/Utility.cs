using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using UPnP.Extensions;

namespace UPnP
{
    public class Utility
    {
        // Emulate http://msdn.microsoft.com/en-us/library/system.security.securityelement.escape.aspx
        // SecurityElement.Escape
        public static string XmlEscape(string input)
        {
            return
                input.Replace("&", "&amp;")
                    .Replace("<", "&lt;")
                    .Replace(">", "&gt;")
                    .Replace("\"", "&quot;")
                    .Replace("\'", "&apos;");
        }

        public static async Task<XElement> DownloadXmlAsync(string uri)
        {
            if (!uri.EndsWith(".xml"))
                throw new ArgumentException("Bad input, please provide path to XML");

            var xml = await new WebClient().DownloadStringAsync(uri);
            var dom = XElement.Parse(xml);
            return dom;
        }
    }
}