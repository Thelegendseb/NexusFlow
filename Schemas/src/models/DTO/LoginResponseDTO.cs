using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusFlow.src.models.DTO
{
    public class LoginResponseDTO
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string AccessToken { get; set; }
    }
}
