using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class OpleidingOffer
    {
        public string IdOpleiding { get; set; }
        public int IdOffer { get; set; }

        public Offers IdOfferNavigation { get; set; }
        public Opleiding IdOpleidingNavigation { get; set; }
    }
}
