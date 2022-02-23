using System.Collections.Generic;
using RouteAPI.DataAccess.Entities;

namespace RouteAPI
{
    public interface IRouteManager
    {
        /// <summary>
        /// Registers a Route 
        /// </summary>
        /// <param name="fromLandMark"></param>
        /// <param name="toLandMark"></param>
        /// <param name="weightedDistance"></param>
        /// <returns>Returns true if registered successfully</returns>
        Route RegisterRoute(string fromLandMark, string toLandMark, int weightedDistance);

        /// <summary>
        /// Gets the distance for a route
        /// </summary>
        /// <param name="route"></param>
        /// <returns>Int</returns>
        int GetDistance(string route);

        /// <summary>
        /// Gets all routes
        /// </summary>
        /// <returns></returns>
        IEnumerable<Route> GetAllRoutes();

        /// <summary>
        /// Gets the number of routes having maximum<param name="maxHops"></param> hops
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="maxHops"></param>
        /// <returns></returns>
        int GetRoutesForLandMarksWithSpecifiedNumberOfHops(string origin, string destination, int maxHops);
    }
}