using System;
using System.Collections.Generic;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RouteAPI;
using RouteAPI.DataAccess.Entities;
using RouteAPI.Entities;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Gets all routes")]
        public IEnumerable<Route> Get()
        {
            return _routeManager.GetAllRoutes();
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
        [SwaggerOperation(Summary = "Add a route")]
        public JsonResult RegisterRoute(string origin, string destination, int distance)
        {
           
            return new JsonResult(_routeManager.RegisterRoute(origin, destination, distance));
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
