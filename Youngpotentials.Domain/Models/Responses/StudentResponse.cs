using System;
using System.Collections.Generic;
using System.Text;

namespace Youngpotentials.Domain.Models.Responses
{
    public class StudentResponse : UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string CvUrl { get; set; }
    }
}
