using System.Linq;

namespace SonosWp8.Navigation
{
    public class RouteProvider : IRouteProvider
    {
        private readonly IRouteMap _map;

        public RouteProvider(IRouteMap map)
        {
            _map = map;
        }

        public IRouteMap GetRouteById(string id)
        {
            return _map.GetRoutes().SingleOrDefault(x => x.Id == id);
        }

        public IRouteMap GetRouteByUri(string uri)
        {
            return _map.GetRoutes().SingleOrDefault(x => x.Uri == uri);
        }
    }
}