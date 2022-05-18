using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using ConsoleApp1;
using GrapgDS;
using RouteAPI.DataAccess;
using RouteAPI.DataAccess.Entities;
using RouteAPI.Entities;
using RouteAPI.Exceptions;

namespace RouteAPI
{
    public class RouteManager : IRouteManager
    {
        private readonly ILandMarkManager _manager;
        private readonly IRoutesRepository _routesRepository;
        private readonly Graph _graph;
        private Dictionary<string, int> pathsCollection;

        private HashSet<Landmark> _landmarks = new HashSet<Landmark>();

        public RouteManager(ILandMarkManager manager, IRoutesRepository routesRepository, Graph graph)
        {
            _manager = manager;
            _routesRepository = routesRepository;
            _graph = graph;
            pathsCollection = new Dictionary<string, int>();
        }

        public IEnumerable<Edge> RegisterRoute(string routes)
        {
            try
            {
                return routes.Split(';').Select(r =>
                {
                    var inputRoute = GetLandmarksFromInputRoute(out var regex);
                    if (inputRoute.origin != null && inputRoute.destination != null && inputRoute.distance != 0)
                    {
                        return _graph.AddEdge(inputRoute.origin, inputRoute.destination, inputRoute.distance);
                    }

                    throw new Exception("Bad request: Invalid route");
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public void ProcessRoutes()
        {
            foreach (var node in _graph.Nodes)
            {
                GetAllPaths(node, pathsCollection, node.Key, _graph, 0);
            }
        }

        private static void GetAllPaths(Node origin, Dictionary<string, int> paths, string psf, Graph graph, int distance)
        {
            foreach (var child in origin.AdjacentNodes)
            {
                if (!psf.Contains(child.Key))
                {
                    GetAllPaths(child, paths, psf + child.Key, graph, distance + graph[origin.Key, child.Key].Distance);
                }
                if (!paths.ContainsKey(psf))
                    paths.Add(psf, distance);
            }
        }


        private  (string origin, string destination, int distance) GetLandmarksFromInputRoute(out string regex)
        {
            regex = @"^([a-zA-Z]{2})(\d{1})$";
            var matches = Regex.Matches(regex, regex);
            if (matches.Count > 0)
            {
                return (matches.FirstOrDefault()?.Groups[0].Value.FirstOrDefault().ToString(),
                    matches.FirstOrDefault()?.Groups[0].Value.LastOrDefault().ToString(),
                    int.Parse(matches.FirstOrDefault()?.Groups[1].Value.FirstOrDefault().ToString() ?? string.Empty));

            }
            return (default, default, default);
        }

        public int GetDistance(string route)
        {
            var inputRoute = GetLandmarksFromInputRoute(out var regex);
            if (!IsRouteValid(inputRoute))
                throw new RouteException(HttpStatusCode.BadRequest, Constants.ExceptionMessageWhenRouteDoesNotExists);

            return pathsCollection[
                pathsCollection.Keys.FirstOrDefault(p =>
                    p.StartsWith(inputRoute.origin) && p.EndsWith(inputRoute.destination)) ?? string.Empty];
        }

        public int GetRoutesForLandMarksWithSpecifiedNumberOfHops(string origin, string destination, int maxHops)
        {
            if (_graph[origin] != null && _graph[destination] != null)
                throw new RouteException(HttpStatusCode.BadRequest, Constants.ExceptionMessageWhenRouteDoesNotExists);

            return
                pathsCollection.Keys.Aggregate(0, (hops, p) =>
                    p.StartsWith(origin) && p.EndsWith(destination) && p.Length < maxHops + 2 ? hops + 1 : hops);
        }

        public void Remove()
        {
            _routesRepository.RemoveAll();
        }

        public void RemoveRoute(string @from, string to)
        {
            _routesRepository.Remove(from, to);
        }

        #region Helper methods



        internal bool IsRouteValid((string origin, string destination, int distance) route)
        {
            return route.origin != null && route.destination != null && route.distance != 0;
        }

        #endregion
    }
}
