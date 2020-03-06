using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Youngpotentials.Domain.Models.Responses
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public bool IsStudent { get; set; }
    }
}
