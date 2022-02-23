using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using GrapgDS;
using RouteAPI.Entities;
using RouteAPI.Exceptions;

namespace RouteAPI
{
    public class RouteManager : IRouteManager
    {
        private readonly ILandMarkManager _manager;

        private HashSet<Landmark> _landmarks = new HashSet<Landmark>();

        private Dictionary<string, int> _routes = new Dictionary<string, int>();

        public RouteManager(ILandMarkManager manager)
        {
            _manager = manager;
        }

        public Route this[string from, string To]
        {
            get
            {
                var fromLandmark = _landmarks.FirstOrDefault(lm => lm.Name.Equals(from, StringComparison.InvariantCultureIgnoreCase));
                var toLandmark = _landmarks.FirstOrDefault(lm => lm.Name.Equals(from, StringComparison.InvariantCultureIgnoreCase));
                return null;
            }
        }

        public bool RegisterRoute(string from, string to, int weightedDistance)
        {
            try
            {
                if (string.Equals(to, from, StringComparison.InvariantCultureIgnoreCase))
                    throw new RouteException(HttpStatusCode.BadRequest, Constants.ExceptionMessageForInvalidRoute);

                var fromLandMark = _manager.RegisterLandMark(from);
                var toLandMark = _manager.RegisterLandMark(to);
                var path = $"{from}-{to}";

                if (_routes.ContainsKey(path))
                    throw new RouteException(HttpStatusCode.BadRequest,
                        Constants.ExceptionMessageWhenRouteAlreadyExists);

                fromLandMark.AdjacentLandmarks.Add(toLandMark);
                _routes.Add($"{from}-{to}", weightedDistance);

                return true;
            }
            catch (RouteException)
            {
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new RouteException(HttpStatusCode.InternalServerError, Constants.ServerError);
            }
        }

        public int GetDistance(string route)
        {
            if (IsRouteValid(route))
                throw new RouteException(HttpStatusCode.BadRequest, Constants.ExceptionMessageWhenRouteDoesNotExists);

            int distance = 0;
            var paths = route.Split("-");
            for (var i = 0; i < paths.Length - 1; i++)
            {
                var path = $"{paths[i]}-{paths[i + 1]}";
                distance += _routes[path];
            }
            return distance;
        }

        public IDictionary<string, int> GetAllRoutes()
        {
            return _routes;
        }

        public int GetRoutesForLandMarksWithSpecifiedNumberOfHops(string origin, string destination, int maxHops)
        {
            var fromLandmark = _manager.GetLandmark(origin);

            if (fromLandmark == null)
                throw new RouteException(HttpStatusCode.BadRequest, Constants.ExceptionMessageWhenRouteDoesNotExists);

            var toLandmark = _manager.GetLandmark(destination);

            if (toLandmark == null)
                throw new RouteException(HttpStatusCode.BadRequest, Constants.ExceptionMessageWhenRouteDoesNotExists);

            var routes = GetRoutes(fromLandmark, toLandmark);
            return routes.Count(r => r.Length <= maxHops + 2);
        }

        #region Helper methods

        internal IEnumerable<string> GetRoutes(Landmark origin, Landmark destination)
        {
            // Collection to back track landmarks
            var backTrackPaths = new Dictionary<string, List<string>>();
            Queue<Landmark> queue = new Queue<Landmark>();
            HashSet<string> results = new HashSet<string>();
            queue.Enqueue(origin);
            HashSet<Landmark> isVisited = new HashSet<Landmark>();
            backTrackPaths.Add(origin.Name, new List<string>() { origin.Name });
            while (queue.Count > 0)
            {
                var source = queue.Dequeue();
                foreach (Landmark neighbor in source.AdjacentLandmarks)
                {
                    foreach (var path in backTrackPaths[source.Name].ToList())
                    {
                        if (!backTrackPaths.ContainsKey(neighbor.Name))
                        {
                            backTrackPaths.Add(neighbor.Name, new List<string>() { path + neighbor.Name });
                        }
                        else
                        {
                            backTrackPaths[neighbor.Name].Add(path + neighbor.Name);
                        }
                    }

                    if (neighbor.Equals(destination))
                    {
                        results.UnionWith(backTrackPaths[neighbor.Name]);
                        continue;
                    }

                    if (!isVisited.Contains(neighbor))
                    {
                        isVisited.Add(neighbor);
                    }
                    queue.Enqueue(neighbor);
                }
            }

            return results;
        }

        internal bool IsRouteValid(string route)
        {
            if (string.IsNullOrEmpty(route))
                return false;

            var regex = @"((\w)+-(\w)+)";
            var isRouteValid = Regex.IsMatch(route, regex);
            if (!isRouteValid)
                return false;

            var paths = route.Split("-");
            var uniquePaths = paths.Distinct().ToArray();

            var landmarks = uniquePaths.Select(lm => _manager.GetLandmark(lm));

            if (uniquePaths.Count() < 2 && landmarks.Any(lm => lm == null)
                                        && uniquePaths.Count() != paths.Length)
                return false;

            return true;
        }

        #endregion


    }
}
