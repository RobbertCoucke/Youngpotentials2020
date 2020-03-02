using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoungpotentialsAPI.Models.Responses
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }
        public string Token { get; set; }
    }
}
