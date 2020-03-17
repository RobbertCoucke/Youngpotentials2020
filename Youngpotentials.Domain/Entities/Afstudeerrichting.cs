using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class Afstudeerrichting
    {
        public Afstudeerrichting()
        {
            AfstudeerrichtingOffer = new HashSet<AfstudeerrichtingOffer>();
            Keuze = new HashSet<Keuze>();
        }

        public string Id { get; set; }
        public string AfstudeerrichtingNaam { get; set; }
        public string OpleidingId { get; set; }

        public virtual Opleiding Opleiding { get; set; }
        public virtual ICollection<AfstudeerrichtingOffer> AfstudeerrichtingOffer { get; set; }
        public virtual ICollection<Keuze> Keuze { get; set; }
    }
}
