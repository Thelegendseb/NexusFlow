using API.Managers;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

using API.Singletons;
using NexusFlow.src.models;
using NexusFlow.src.models.DB;
using NexusFlow.src.models.DTO;
using Schemas.src.models.DTO;
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

        [HttpGet]
        public NexusNodeDTO GetRoot(string accesstoken)
        {
             return NodeManager.GetUserRoot(accesstoken);
        }

        [HttpGet]
        public List<NexusNodeDTO> GetChildNodes(string accesstoken, string parentid)
        {
            return NodeManager.GetChildNodes(accesstoken,parentid);
        }

        [HttpGet]
        public NexusNodeDataDTO GetNodeData(string accesstoken, string nodeid)
        {
            return NodeManager.GetNodeData(accesstoken, nodeid);
        }

        [HttpPost]
        public IActionResult AddNode([FromBody] NexusNodeCreationDTO newNode, string accesstoken, string parentid)
        {
            return NodeManager.AddNode(newNode, accesstoken, parentid);
        }

        [HttpDelete]
        public IActionResult DeleteNode(string accesstoken, string nodeid)
        {
            return NodeManager.DeleteNode(accesstoken, nodeid);
        }

        [HttpPost]
        public IActionResult EditNode([FromBody] NexusNodeCreationDTO newNode, string accesstoken, string nodeid)
        {
            return NodeManager.EditNode(newNode, accesstoken, nodeid);
        }
    }
}   