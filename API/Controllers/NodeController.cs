using API.Managers;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTO;

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
        public List<NexusNodeDTO> GetChildren(string accesstoken, string parentid)
        {
            return NodeManager.GetChildNodes(accesstoken,parentid);
        }

        [HttpGet]
        public NexusNodeDataDTO GetData(string accesstoken, string nodeid)
        {
            return NodeManager.GetNodeData(accesstoken, nodeid);
        }

        [HttpPost]
        public IActionResult Add([FromBody] NexusNodeCreationDTO newNode, string accesstoken, string parentid)
        {
            return NodeManager.AddNode(newNode, accesstoken, parentid);
        }

        [HttpDelete]
        public IActionResult Delete(string accesstoken, string nodeid)
        {
            return NodeManager.DeleteNode(accesstoken, nodeid);
        }

        [HttpPost]
        public IActionResult Edit([FromBody] NexusNodeCreationDTO newNode, string accesstoken, string nodeid)
        {
            return NodeManager.EditNode(newNode, accesstoken, nodeid);
        }

    }
}   