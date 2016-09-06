namespace SonosWp8.Models
{
    public class DeviceAuthTokenNeededEventArgs
    {
        public DeviceAuthTokenNeededEventArgs(bool isNeeded, string deviceLinkUrl)
        {
            IsNeeded = isNeeded;
            DeviceLinkUrl = deviceLinkUrl;
        }

        public bool IsNeeded { get; set; }
        public string DeviceLinkUrl { get; set; }
    }
}