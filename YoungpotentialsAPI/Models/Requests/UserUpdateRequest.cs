using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoungpotentialsAPI.Models.Requests
{
    public class UserUpdateRequest
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
