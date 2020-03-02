using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.Domain.Entities;
using Type = Youngpotentials.Domain.Entities.Type;

namespace Youngpotentials.Domain.Models.Responses
{
    public class OfferResponse
    {
        public int Id { get; set; }
        public int? AttachmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public bool Verified { get; set; }
        public string Address { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Code { get; set; }
        public int? CompanyId { get; set; }
        public int TypeId { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public bool? Country { get; set; }
        public virtual Companies Company { get; set; }
        public virtual Type Type { get; set; }
    }
}
