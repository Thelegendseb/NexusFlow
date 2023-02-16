using API.Managers;
using Microsoft.AspNetCore.Mvc;
using NexusFlow.src.core;
using System.Collections.Generic;

using API.Singletons;
using NexusFlow.src.models;
using NexusFlow.src.models.DB;
using NexusFlow.src.models.DTO;

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
        public NexusNodeDB GetRoot(string accesstoken)
        {
             return NodeManager.GetUserRoot(accesstoken);
        }

    }
}