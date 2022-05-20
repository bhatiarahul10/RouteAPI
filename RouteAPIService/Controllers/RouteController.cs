using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RouteAPI;
using RouteAPI.DataAccess.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace RouteAPIService.Controllers
{
    [ApiController]
    
    public class RouteController : ControllerBase
    {
        private readonly ILogger<RouteController> _logger;
        private readonly IRouteManager _routeManager;

        public RouteController(ILogger<RouteController> logger, IRouteManager routeManager)
        {
            _logger = logger;
            _routeManager = routeManager;
        }

        [HttpGet]
        [Route("v1/[controller]/distance")]
        [SwaggerOperation(Summary = "Gets distance for a route")]
        public int GetDistance(string route)
        {
            return _routeManager.GetDistance(route);
        }

        [HttpGet]
        [Route("v1/[controller]/count")]
        [SwaggerOperation(Summary = "Gets all routes with a specified origin and destination having a specific number of maximum hops")]
        public int GetRoutesWithSpecifiedNumberOfHops(string origin, string destination, int stops)
        {
            return _routeManager.GetRoutesForLandMarksWithSpecifiedNumberOfHops(origin, destination, stops);
        }

        [HttpPost]
        [Route("v1/route")]
        [SwaggerOperation(Summary = "Add multiple routes seperated by semicolon")]
        public async Task<JsonResult> RegisterRoutes(string routes)
        {
            var a = await _routeManager.RegisterRoute(routes);
            return new JsonResult(a);
        }

        [HttpDelete]
        [Route("v1/route/{origin}/{destination}")]
        [SwaggerOperation(Summary = "Delete a route")]

        public void DeleteRoute(string origin, string destination)
        {
             _routeManager.RemoveRoute(origin, destination);
        }

        [HttpDelete]
        [Route("v1/route")]
        [SwaggerOperation(Summary = "Delete all routes")]
        public void DeleteAllRoutes()
        {
            _routeManager.Remove();
        }
    }
}
