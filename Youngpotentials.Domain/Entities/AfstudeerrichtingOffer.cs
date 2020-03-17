using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class AfstudeerrichtingOffer
    {
        public string IdAfstudeerrichting { get; set; }
        public int IdOffer { get; set; }

        public virtual Afstudeerrichting IdAfstudeerrichtingNavigation { get; set; }
        public virtual Offers IdOfferNavigation { get; set; }
    }
}
