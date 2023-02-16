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

        // ADD NODE
        [HttpPost]
        public List<NexusNodeDTO> AddNode([FromBody] NexusNodeDTO newNode,string data,string accesstoken, string parentid)
        {
            // return NodeManager.GetChildNodes(accesstoken, parentid);
        }

        // DELETE NODE
        [HttpDelete]
        public List<NexusNodeDTO> DeleteNode(string accesstoken, string nodeid)
        {
            // return NodeManager.GetChildNodes(accesstoken, parentid);
        }

        // EDIT NODE
        public List<NexusNodeDTO> EditNode([FromBody] NexusNodeDTO newNode, string data, string accesstoken, string parentid)
        {
            // return NodeManager.GetChildNodes(accesstoken, parentid);
        }


    }
}