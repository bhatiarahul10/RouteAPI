using System.Collections.Generic;
using System.Threading.Tasks;
using RouteAPI.DataAccess.Entities;

namespace RouteAPI
{
    public interface IRouteManager
    {
        Task<IEnumerable<Edge>> RegisterRoute(string routes);

        int GetDistance(string route);

        int GetRoutesForLandMarksWithSpecifiedNumberOfHops(string origin, string destination, int maxHops);

        void Remove();

        void RemoveRoute(string from, string to);
    }
}