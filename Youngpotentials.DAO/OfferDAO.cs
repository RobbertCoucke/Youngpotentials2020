using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.DAO
{
    public interface IOfferDAO
    {
        IEnumerable<Offers> GetAllOffers();
        IEnumerable<Offers> GetAllOffersByCompany(int id);
        Offers GetOffersById(int id);
        Offers CreateOffer(Offers offer);
        void UpdateOffer(Offers offer);
        void DeleteOffer(int id);
        IEnumerable<Offers> GetOffersByTagId(string id);
        IEnumerable<Offers> GetOffersByStudiegebiedId(string id);
        IEnumerable<Offers> GetOffersByOpleidingId(string id);
        IEnumerable<Offers> GetOffersByAfstudeerrichtingId(string id);
        IEnumerable<Offers> GetOffersByKeuzeId(string id);
        IEnumerable<Offers> GetOffersByTags(IList<string> ids);
    }
    public class OfferDAO : IOfferDAO
    {

        private YoungpotentialsContext _db;

        public OfferDAO()
        {
            _db = new YoungpotentialsContext();
        }

        public Offers CreateOffer(Offers offer)
        {
            _db.Entry(offer).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _db.SaveChanges();
            return offer;
        }

        public void DeleteOffer(int id)
        {
            var offer = _db.Offers.Where(o => o.Id == id).FirstOrDefault();
            _db.Offers.Remove(offer);
            _db.SaveChanges();
        }

        public IEnumerable<Offers> GetAllOffers()
        {
            return _db.Offers.ToList();
        }

        public IEnumerable<Offers> GetAllOffersByCompany(int id)
        {
            return _db.Offers.Where(o => o.CompanyId == id).ToList();
        }

        public Offers GetOffersById(int id)
        {
            return _db.Offers.Where(o => o.Id == id).FirstOrDefault();
        }

        public void UpdateOffer(Offers offer)
        {
            _db.Entry(offer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Offers> GetOffersByStudiegebiedId(string id)
        {
            return _db.StudiegebiedOffer.Where(o => o.IdStudiegebied == id).Include(o => o.Offer).ToList();
        }
        public IEnumerable<Offers> GetOffersByOpleidingId(string id)
        {
            return _db.OpleidingOffer.Where(o => o.IdOpleiding == id).Include(o => o.Offer).ToList();
        }
        public IEnumerable<Offers> GetOffersByAfstudeerrichtingId(string id)
        {
            return _db.AfstudeerrichtingOffer.Where(o => o.IdAfstudeerrichting == id).Include(o => o.Offer).ToList();
        }
        public IEnumerable<Offers> GetOffersByKeuzeId(string id)
        {
            return _db.KeuzeOffer.Where(o => o.IdKeuze == id).Include(o => o.Offer).ToList();
        }
        public IEnumerable<Offers> GetOffersByTags(IList<string> ids)
        {
            foreach (string id in ids)
            {
                GetOffersByTagId(id);
            }
        }
    }
}
