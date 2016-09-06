namespace SonosWp8.Navigation
{
    public interface IRouteProvider
    {
        SonosWp8.Navigation.IRouteMap GetRouteById(string id);
    }
}