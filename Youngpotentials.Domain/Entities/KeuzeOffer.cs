using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class KeuzeOffer
    {
        public string IdKeuze { get; set; }
        public int IdOffer { get; set; }

        public Keuze IdKeuzeNavigation { get; set; }
        public Offers IdOfferNavigation { get; set; }
    }
}
