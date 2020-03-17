using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class AfstudeerrichtingOffer
    {
        public string IdAfstudeerrichting { get; set; }
        public int IdOffer { get; set; }

        public Afstudeerrichting IdAfstudeerrichtingNavigation { get; set; }
        public Offers IdOfferNavigation { get; set; }
    }
}
