using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class Studiegebied
    {
        public Studiegebied()
        {
            Opleiding = new HashSet<Opleiding>();
            StudiegebiedOffer = new HashSet<StudiegebiedOffer>();
        }

        public string Id { get; set; }
        public string Studiegebied1 { get; set; }
        public string Kleur { get; set; }
        public bool? IsGraduate { get; set; }

        public virtual ICollection<Opleiding> Opleiding { get; set; }
        public virtual ICollection<StudiegebiedOffer> StudiegebiedOffer { get; set; }
    }
}
