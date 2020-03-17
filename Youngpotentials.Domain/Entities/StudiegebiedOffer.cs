using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class StudiegebiedOffer
    {
        public string IdStudiegebied { get; set; }
        public int IdOffer { get; set; }

        public virtual Offers IdOfferNavigation { get; set; }
        public virtual Studiegebied IdStudiegebiedNavigation { get; set; }
    }
}
