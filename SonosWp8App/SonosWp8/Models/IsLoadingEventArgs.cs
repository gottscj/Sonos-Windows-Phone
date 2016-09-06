namespace SonosWp8.Models
{
    public class IsLoadingEventArgs
    {
        public IsLoadingEventArgs() : this("", false)
        {
        }

        public IsLoadingEventArgs(string text, bool loading)
        {
            Text = text;
            Loading = loading;
        }

        public string Text { get; set; }
        public bool Loading { get; set; }
    }
}