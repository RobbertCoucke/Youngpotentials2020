using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class Keuze
    {
        public Keuze()
        {
            KeuzeOffer = new HashSet<KeuzeOffer>();
        }

        public string Id { get; set; }
        public string Keuze1 { get; set; }
        public string AfstudeerrichtingId { get; set; }

        public Afstudeerrichting Afstudeerrichting { get; set; }
        public ICollection<KeuzeOffer> KeuzeOffer { get; set; }
    }
}
