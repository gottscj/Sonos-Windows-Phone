using System;
using System.Collections.Generic;

namespace SonosWp8.Navigation
{
    public interface IRouteMap
    {
        string Id { get; }
        string Uri { get; }
        Type Type { get; }
        IReadOnlyCollection<IRouteMap> GetRoutes();
    }
}