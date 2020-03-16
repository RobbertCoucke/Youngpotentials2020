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

        public Opleiding Opleiding { get; set; }
        public ICollection<AfstudeerrichtingOffer> AfstudeerrichtingOffer { get; set; }
        public ICollection<Keuze> Keuze { get; set; }
    }
}
