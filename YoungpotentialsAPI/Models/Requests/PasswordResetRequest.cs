using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoungpotentialsAPI.Models.Requests
{
    public class PasswordResetRequest
    {
        public string token { get; set; }
        public string email { get; set; }
        public string newPassword { get; set; }
    }
}
