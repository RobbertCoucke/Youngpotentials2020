using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class Companies
    {
        public Companies()
        {
            Offers = new HashSet<Offers>();
        }

        public int? Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string CompanyName { get; set; }
        public int? UserId { get; set; }
        public bool? Verified { get; set; }
        public int? SectorId { get; set; }

        public virtual Sector Sector { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<Offers> Offers { get; set; }
    }
}
