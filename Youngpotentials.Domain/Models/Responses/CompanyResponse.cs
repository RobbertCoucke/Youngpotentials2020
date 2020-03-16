using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.Domain.Models.Responses
{
    public class CompanyResponse : UserResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string CompanyName { get; set; }
        public Boolean Verified { get; set; }

        [JsonIgnore]
        public Sector Sector { get; set; }

        //public IEnumerable<Offers> Offers { get; set; }
    }
}
