using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RouteAPI;
using RouteAPI.Entities;

namespace RouteAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        [Route("/")]
        public IEnumerable<Route> Get(string route)
        {
            return new List<Route>();
        }

        [HttpGet]
        [Route("/distance")]
        public int GetDistance()
        {
            return 12;
        }

        [HttpGet]
        [Route("/{stops?}")]
        public int GetRoutesWithSpecifiedNumberOfHops(string origin, string destination, int stops)
        {
            return _routeManager.GetRoutesForLandMarksWithSpecifiedNumberOfHops(origin, destination, stops);
        }


        [HttpPost]
        public void RegisterRoute(Route route)
        {

        }
    }
}
