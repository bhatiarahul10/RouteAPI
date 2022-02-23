using System.Collections.Generic;
using RouteAPI.DataAccess.Entities;
using RouteAPI.Entities;

namespace RouteAPI.DataAccess
{
    public interface IRoutesRepository
    {
        IEnumerable<Route> GetRoutes();

        Route GetRoute(string from, string to);

        Route SaveRoute(string from ,string to, int distance);

        void RemoveAll();

        void Remove(string from, string to);
    }
}