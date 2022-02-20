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

        private List<Route> _routes = new List<Route>();

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
                return _routes.FirstOrDefault();
            }
        }


        public bool RegisterRoute(string from, string to, int weightedDistance)
        {
            try
            {
                if (String.Equals(to, from, StringComparison.InvariantCultureIgnoreCase))
                    throw new InvalidRouteException();

                if (DoesRouteExists(from, to))
                    throw new RouteAlreadyExistsException();

                var fromLandMark = _manager.RegisterLandMark(from);
                var toLandMark = _manager.RegisterLandMark(to);

                var route = new Route(fromLandMark, toLandMark, weightedDistance);
                _routes.Add(route);
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
                throw new ArgumentNullException("Empty route");

            var landMarks = route.ToCharArray();
            return 0;
        }

        #region Helper methods

        private bool DoesRouteExists(string from, string to)
        {
            return true;
        }

        #endregion 


    }
}
