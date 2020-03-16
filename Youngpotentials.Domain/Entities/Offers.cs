using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class Offers
    {
        public Offers()
        {
            AfstudeerrichtingOffer = new HashSet<AfstudeerrichtingOffer>();
            Applications = new HashSet<Applications>();
            Favorites = new HashSet<Favorites>();
            KeuzeOffer = new HashSet<KeuzeOffer>();
            OpleidingOffer = new HashSet<OpleidingOffer>();
            StudiegebiedOffer = new HashSet<StudiegebiedOffer>();
        }

        public int Id { get; set; }
        public int? AttachmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public bool Verified { get; set; }
        public string Address { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Code { get; set; }
        public int? CompanyId { get; set; }
        public int? TypeId { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public Companies Company { get; set; }
        public Type Type { get; set; }
        public ICollection<AfstudeerrichtingOffer> AfstudeerrichtingOffer { get; set; }
        public ICollection<Applications> Applications { get; set; }
        public ICollection<Favorites> Favorites { get; set; }
        public ICollection<KeuzeOffer> KeuzeOffer { get; set; }
        public ICollection<OpleidingOffer> OpleidingOffer { get; set; }
        public ICollection<StudiegebiedOffer> StudiegebiedOffer { get; set; }
    }
}
