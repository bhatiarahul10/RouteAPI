using System;
using System.Collections.Generic;
using System.Linq;
using RouteAPI.Entities;

namespace RouteAPI.DataAccess
{
    public class RoutesRepository : IRoutesRepository
    {
        private readonly Dictionary<string, int> _routes;

        public RoutesRepository()
        {
            _routes = new Dictionary<string, int>();
        }

        public Dictionary<string, int> GetRoutes()
        {
            return _routes;
        }

        public KeyValuePair<string, int> GetRoute(string @from, string to)
        {
            var path = $"{from}-{to}";
            return _routes.FirstOrDefault(k => string.Equals(path, k.Key, StringComparison.InvariantCultureIgnoreCase));
        }

        public void SaveRoute(string @from, string to, int distance)
        {
            _routes.Add($"{from}-{to}", distance);
        }

    }
}
