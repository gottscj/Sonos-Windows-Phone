using System;

namespace UPnP.Models
{
    public class Description
    {
        public Description()
        {
            BaseAddress = null;
            ServiceType = string.Empty;
            ServiceId = string.Empty;
            SCP = string.Empty;
            ControlURL = string.Empty;
            EventSubURL = string.Empty;
        }

        public Uri BaseAddress { get; set; }
        public string ServiceType { get; set; }
        public string ServiceId { get; set; }
        public string SCP { get; set; }
        public string ControlURL { get; set; }
        public string EventSubURL { get; set; }
    }
}