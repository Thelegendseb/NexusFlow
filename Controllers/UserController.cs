using API.Managers;
using Microsoft.AspNetCore.Mvc;
using NexusFlow.src.core;
using System.Collections.Generic;

using API.Singletons;
using NexusFlow.src.models;

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
        public void Create(string name)
        {
             UserManager.CreateUser(name);
        }

    }
}