using System;
using System.Collections.Generic;
using System.Text;

namespace Youngpotentials.Domain.Models.Responses
{
    public class CompanyResponse : UserResponse
    {
        public string Description { get; set; }
        public string Url { get; set; }
        public string CompanyName { get; set; }
    }
}
