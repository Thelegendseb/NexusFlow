using API.Managers;
using Microsoft.AspNetCore.Mvc;
using NexusFlow.src.core;
using System.Collections.Generic;

using API.Singletons;
using NexusFlow.src.models;

namespace API.Controllers
{
    [ApiController]
    [Route("API/Nodes/[action]")]
    public class NodeController : ControllerBase
    {
        private readonly ILogger<NodeController> _logger;

        public NodeController(ILogger<NodeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public void SomeMethod()
        {
             NodeManager.SomeMethod();
        }

    }
}