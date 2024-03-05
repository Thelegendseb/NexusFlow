using API.Managers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdminController : Controller
    {
        [ApiController]
        [Route("API/Admin/[action]")]
        public class NodeController : ControllerBase
        {
            private readonly ILogger<NodeController> _logger;

            public NodeController(ILogger<NodeController> logger)
            {
                _logger = logger;
            }

            [HttpPost]
            public IActionResult ClearAll()
            {
                return AdminManager.ClearAll();
            }
        }

    }
}
