using System;
using System.Collections.Generic;
using System.Linq;
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
                    throw new InvalidRouteException();

                var fromLandMark = _manager.RegisterLandMark(from);
                var toLandMark = _manager.RegisterLandMark(to);
                var path = $"{from}-{to}";

                if (_routes.ContainsKey(path))
                    throw new RouteAlreadyExistsException();

                fromLandMark.AdjacentLandmarks.Add(toLandMark);
                _routes.Add($"{from}-{to}", weightedDistance);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public int GetDistance(string route)
        {
            if (string.IsNullOrEmpty(route))
                throw new RouteDoesNotExistException();

            var paths = route.Split("-");
            var uniquePaths = paths.Distinct().ToArray();

            var landmarks = uniquePaths.Select(lm => _manager.GetLandmark(lm));

            if (uniquePaths.Count() < 2 && landmarks.Any(lm => lm == null)
                                         && uniquePaths.Count() != paths.Length)
                throw new RouteDoesNotExistException();

            int distance = 0;
            for (var i = 0; i < uniquePaths.Length - 1; i++)
            {
                var path = $"{uniquePaths[i]}-{uniquePaths[i + 1]}";
                distance += _routes[path];
            }
            return distance;
        }

        public int GetRoutesForLandMarksWithSpecifiedNumberOfHops(string origin, string destination, int maxHops)
        {
            var fromLandmark = _manager.GetLandmark(origin);

            if (fromLandmark == null)
                throw new RouteDoesNotExistException();

            var toLandmark = _manager.GetLandmark(destination);

            if (toLandmark == null)
                throw new RouteDoesNotExistException();

            var routes = GetRoutes(fromLandmark, toLandmark);
            return routes.Count(r => r.Length <= maxHops);
        }

        #region Helper methods

        public IEnumerable<string> GetRoutes(Landmark origin, Landmark destination)
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

        #endregion


    }
}
