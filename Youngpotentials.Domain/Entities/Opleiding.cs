using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class Opleiding
    {
        public Opleiding()
        {
            Afstudeerrichting = new HashSet<Afstudeerrichting>();
            OpleidingOffer = new HashSet<OpleidingOffer>();
        }

        public string Id { get; set; }
        public string NaamOpleiding { get; set; }
        public string IdStudiegebied { get; set; }

        public Studiegebied IdStudiegebiedNavigation { get; set; }
        public ICollection<Afstudeerrichting> Afstudeerrichting { get; set; }
        public ICollection<OpleidingOffer> OpleidingOffer { get; set; }
    }
}
