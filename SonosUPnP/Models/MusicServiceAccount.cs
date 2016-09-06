namespace Sonos.Models
{
    public class MusicServiceAccount
    {
        public MusicServiceAccount(string serviceId, string userName)
        {
            ServiceId = serviceId;
            UserName = userName;
        }

        public string ServiceId { get; set; }
        public string UserName { get; set; }

        public override string ToString()
        {
            return "Id: " + ServiceId + " Username: " + UserName;
        }
    }
}