using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class Applications
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public int StudentId { get; set; }

        public Offers Offer { get; set; }
        public Students Student { get; set; }
    }
}
