using System.Linq;
using System.Xml.Linq;

namespace UPnP.Extensions
{
    public static class StateVariableExtensions
    {
        public static string GetStateVarValue(this StateVariableChangedEventArgs e)
        {
            var value = string.Empty;
            var xml = e.StateVarValue;
            var element = xml.Descendants(XName.Get(e.StateVarName)).FirstOrDefault();
            if (element != null)
            {
                value = element.Value;
            }

            return value;
        }
    }
}