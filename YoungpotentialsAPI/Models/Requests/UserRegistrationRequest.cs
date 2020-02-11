using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YoungpotentialsAPI.Models.Requests
{
    public class UserRegistrationRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
