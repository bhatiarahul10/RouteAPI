using System;
using System.Collections.Generic;
using System.Linq;
using RouteAPI.DataAccess.Entities;
using RouteAPI.Entities;

namespace RouteAPI.DataAccess
{
    public class RoutesRepository : IRoutesRepository
    {
        private readonly Dictionary<string, Route> _routes;

        public RoutesRepository()
        {
            _routes = new Dictionary<string, Route>();
        }

        public IEnumerable<Route> GetRoutes()
        {
            return _routes.Values;
        }

        public Route GetRoute(string @from, string to)
        {
            var path = $"{from}-{to}";
            try
            {
                return _routes[path];
            }
            catch
            {
                return null;
            }
            
        }

        public Route SaveRoute(string @from, string to, int distance)
        {
            var route = new Route(from, to, distance);
            _routes.Add(route.ToString(), route);
            return route;
        }

        public void RemoveAll()
        {
            try
            {
                _routes.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Remove(string @from, string to)
        {
            try
            {
                var route = GetRoute(from, to);
                _routes.Remove(route.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

    }
}
