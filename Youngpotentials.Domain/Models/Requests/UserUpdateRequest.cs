using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Youngpotentials.Domain.Models.Requests
{
    public class UserUpdateRequest
    {
        //USER

        public string Email { get; set; }
       
        //public string Password { get; set; }

        public string Telephone { get; set; }


        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Address { get; set; }


        //COMPANY
        public string Description { get; set; }
        public string Url { get; set; }
        public string CompanyName { get; set; }

        //STUDENT

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string CvUrl { get; set; }

        public bool IsStudent { get; set; }

    }
}
