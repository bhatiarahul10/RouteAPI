using System.Collections.Generic;
using RouteAPI.Entities;

namespace RouteAPI.DataAccess
{
    public interface IRoutesRepository
    {
        Dictionary<string, int> GetRoutes();

        KeyValuePair<string,int> GetRoute(string from, string to);

        void SaveRoute(string from ,string to, int distance);
    }
}