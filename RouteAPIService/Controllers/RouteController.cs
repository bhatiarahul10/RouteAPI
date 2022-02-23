using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RouteAPI;
using RouteAPI.Entities;

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
        [Route("v1/[controller]")]
        public IDictionary<string,int> Get()
        {
            return _routeManager.GetAllRoutes();
        }

        [HttpGet]
        [Route("v1/[controller]/distance")]
        public int GetDistance(string route)
        {
            return _routeManager.GetDistance(route);
        }

        [HttpGet]
        [Route("v1/[controller]/count")]
        public int GetRoutesWithSpecifiedNumberOfHops(string origin, string destination, int stops)
        {
            return _routeManager.GetRoutesForLandMarksWithSpecifiedNumberOfHops(origin, destination, stops);
        }

        [HttpPost]
        [Route("v1/route")]
        public bool RegisterRoute(string origin, string destination, int distance)
        {
            return _routeManager.RegisterRoute(origin, destination, distance);
        }
    }
}
