using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class StudiegebiedOffer
    {
        public string IdStudiegebied { get; set; }
        public int IdOffer { get; set; }

        public Offers IdOfferNavigation { get; set; }
        public Studiegebied IdStudiegebiedNavigation { get; set; }
    }
}
