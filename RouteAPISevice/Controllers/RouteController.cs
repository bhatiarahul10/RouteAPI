using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAPI;
using RouteAPI.Entities;

namespace RouteAPISevice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly ILogger<RouteController> _logger;

        public RouteController(ILogger<RouteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/")]
        public IEnumerable<Route> Get()
        {
            return new List<Route>();
        }

        [HttpGet]
        [Route("/distance")]
        public int GetDistance()
        {
            return 12;
        }


        [HttpPost]
        public void RegisterRoute(Route route)
        {

        }
    }
}
