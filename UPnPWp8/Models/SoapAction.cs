namespace UPnP.Models
{
    public class SoapAction
    {
        public string Name { get; set; }
        public string[] ArgNames { get; set; }
        public int ExpectedReplyCount { get; set; }
    }
}