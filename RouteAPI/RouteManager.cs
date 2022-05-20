using GrapgDS;
using RouteAPI.DataAccess;
using RouteAPI.DataAccess.Entities;
using RouteAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RouteAPI
{
    public class RouteManager : IRouteManager
    {
        private readonly IRoutesRepository _routesRepository;
        private readonly Graph _graph;
        private readonly Dictionary<string, int> _pathsCollection;


        public RouteManager(IRoutesRepository routesRepository)
        {
            _routesRepository = routesRepository;
            _graph = new Graph();
            _pathsCollection = new Dictionary<string, int>();
        }

        public async Task<IEnumerable<Edge>> RegisterRoute(string routes)
        {
            try
            {
                var impactedLandmarks = new HashSet<string>();
                var edgesTasks = routes.Split(',').Select(async r =>
                {
                    var inputRoute = GetLandmarksFromInputRoute(r.Trim());
                    if (inputRoute.origin != null && inputRoute.destination != null && inputRoute.distance != 0)
                    {
                        if (_graph[inputRoute.origin, inputRoute.destination] != null)
                            throw new RouteException(HttpStatusCode.BadRequest, "Route exists");

                        if (string.Equals(inputRoute.origin, inputRoute.destination, StringComparison.InvariantCultureIgnoreCase))
                            throw new RouteException(HttpStatusCode.BadRequest, "Invalid Route");

                        impactedLandmarks.Add(inputRoute.origin);
                        return await _graph.AddEdge(inputRoute.origin, inputRoute.destination, inputRoute.distance);
                    }

                    throw new RouteException(HttpStatusCode.BadRequest, "Invalid Route");
                });

                var edges = await Task.WhenAll(edgesTasks);

                var tempPathsCollection = _pathsCollection.Count > 0 ? _pathsCollection.ToDictionary(a => a.Key, a => a.Value) : _pathsCollection;
                ProcessRoutes(impactedLandmarks);

                if (tempPathsCollection != _pathsCollection)
                    Interlocked.Exchange(ref tempPathsCollection, _pathsCollection);

                return edges;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new RouteException(HttpStatusCode.InternalServerError, "Something wrong happened, Please try again");
            }

        }

        public void ProcessRoutes(ICollection<string> nodes)
        {
            var tempPathsCollection = _pathsCollection.Count > 0 ? _pathsCollection.ToDictionary(a => a.Key, a => a.Value) : _pathsCollection;
            
            foreach (var nodeKey in nodes)
            {
                var node = _graph[nodeKey];
                GetAllPaths(node, tempPathsCollection, node.Key, _graph, 0);
            }

            if (tempPathsCollection != _pathsCollection)
                Interlocked.Exchange(ref tempPathsCollection, _pathsCollection);
        }

        private static void GetAllPaths(Node origin, Dictionary<string, int> paths, string psf, Graph graph, int distance)
        {
            foreach (var child in origin.AdjacentNodes)
            {
                if (!psf.Contains(child.Key))
                {
                    GetAllPaths(child, paths, psf + child.Key, graph, distance + graph[origin.Key, child.Key].Distance);
                }
                if (!paths.ContainsKey(psf) && !string.Equals(psf, origin.Key, StringComparison.InvariantCultureIgnoreCase))
                    paths.Add(psf, distance);
            }
        }


        private (string origin, string destination, int distance) GetLandmarksFromInputRoute(string route)
        {
            var regexWithDistance = @"^([a-zA-Z]{2})(\d{1})$";
            var matches = Regex.Matches(route, regexWithDistance);
            if (matches.Count > 0)
            {
                return (matches.FirstOrDefault()?.Groups[1].Value.FirstOrDefault().ToString(),
                    matches.FirstOrDefault()?.Groups[1].Value.LastOrDefault().ToString(),
                    int.Parse(matches.FirstOrDefault()?.Groups[2].Value.FirstOrDefault().ToString() ?? string.Empty));

            }
            return (default, default, default);
        }

        public int GetDistance(string route)
        {
            try
            {
                return _pathsCollection[route];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new RouteException(HttpStatusCode.BadRequest, "Invalid route");
            }

        }

        public int GetRoutesForLandMarksWithSpecifiedNumberOfHops(string origin, string destination, int maxHops)
        {
            if (_graph[origin] == null && _graph[destination] == null)
                throw new RouteException(HttpStatusCode.BadRequest, Constants.ExceptionMessageWhenRouteDoesNotExists);

            return
                _pathsCollection.Keys.Aggregate(0, (hops, p) =>
                    p.StartsWith(origin) && p.EndsWith(destination) && p.Length <= maxHops + 2 ? hops + 1 : hops);
        }

        public void Remove()
        {
            _routesRepository.RemoveAll();
        }

        public void RemoveRoute(string @from, string to)
        {
            ProcessRoutes(new[] {from});
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
