using System;
using System.Collections.Generic;
using System.Linq;
using GrapgDS;
using RouteAPI.Entities;

namespace RouteAPI
{
    public class RouteManager: IRouteManager
    {
        private readonly ILandMarkManager _manager;

        private List<Landmark> _landmarks = new List<Landmark>();

        private List<Route> _routes = new List<Route>();

        public RouteManager(ILandMarkManager manager)
        {
            _manager = manager;
        }

        public bool RegisterRoute(string from, string to, int weightedDistance)
        {
            try
            {
                if (String.Equals(to, from, StringComparison.InvariantCultureIgnoreCase))
                    throw new InvalidOperationException(Constants.ExceptionMessageForInvalidRoute);

                var fromLandMark = _manager.RegisterLandMark(from);
                var toLandMark = _manager.RegisterLandMark(to);
                var route = new Route(fromLandMark, toLandMark, weightedDistance);
                _routes.Add(route);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }



        public Landmark AddLandMark(string name)
        {
            var landMark = new Landmark(name);
            _landmarks.Add(landMark);
            return landMark;
        }

        public void AddDistanceLandMark(string to , string from, int distance)
        {
            var toLandMark = _landmarks.FirstOrDefault(a => a.Name.Equals(to, StringComparison.InvariantCultureIgnoreCase));
            if (toLandMark == null)
                throw new ArgumentNullException("Non-existent landmark");

            var fromLandMark = _landmarks.FirstOrDefault(a => a.Name.Equals(from, StringComparison.InvariantCultureIgnoreCase));
            if (fromLandMark == null)
                throw new ArgumentNullException("Non existent landmark");

            //var distanceBwLandMarks = new Distance(fromLandMark, toLandMark, distance);

            //_distances.Add(distanceBwLandMarks);
        }

        public int? GetDistance(string to, string from)
        {
            var toLandMark = _landmarks.FirstOrDefault(a => a.Equals(to));
            if (toLandMark == null)
                throw new ArgumentNullException("Non existent landmark");

            var fromLandMark = _landmarks.FirstOrDefault(a => a.Equals(from));
            if (fromLandMark == null)
                throw new ArgumentNullException("Non existent landmark");

            return 0;
        }

       
    }
}
