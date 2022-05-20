using Microsoft.AspNetCore.Mvc;
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
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Get()
        {
            return new JsonResult(_manager.GetLandmarks());
        }

        [HttpPost]
        [Route("v1/[controller]")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult RegisterLandMark(string landmark)
        {
            return new JsonResult(_manager.RegisterLandMark(landmark));
        }
    }
}
