using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using RouteAPI;

namespace RouteAPIService.Controllers
{
    [ApiController]
    public class LandmarkController : Controller
    {
        private readonly ILandMarkManager _manager;

        public LandmarkController(ILandMarkManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("v1/[controller]")]
        public IActionResult Get()
        {
            return new JsonResult(_manager.GetLandmarks());
        }

        [HttpPost]
        [Route("v1/[controller]")]
        public IActionResult RegisterLandMark(string landmark)
        {
            return new JsonResult(_manager.RegisterLandMark(landmark));
        }
    }
}
