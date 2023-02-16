using API.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NexusFlow.src.models.DTO;
using Data.src.models.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("API/Users/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserDTO user)
        {
            return UserManager.CreateUser(user);
        }

        [HttpPost]
        public LoginResponseDTO Login([FromBody] LoginDTO logininfo)
        {
            return UserManager.LoginUser(logininfo);
        }

    }
}