using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youngpotentials.Domain.Entities;
using Type = Youngpotentials.Domain.Entities.Type;

namespace Youngpotentials.DAO
{
    public interface IOfferDAO
    {
        IEnumerable<Offers> GetAllOffers();
        IEnumerable<Offers> GetAllOffersByCompany(int id);
        Offers GetOfferById(int id);
        Offers CreateOffer(Offers offer);
        void UpdateOffer(Offers offer);
        void DeleteOffer(int id);
        IEnumerable<Offers> GetOffersByStudiegebiedId(string id);
        IEnumerable<Offers> GetOffersByOpleidingId(string id);
        IEnumerable<Offers> GetOffersByAfstudeerrichtingId(string id);
        IEnumerable<Offers> GetOffersByKeuzeId(string id);
        IEnumerable<Offers> GetOffersByType(Type type);
        void CreateOfferStudiegebied(StudiegebiedOffer entity);
        void CreateOfferOpleiding(OpleidingOffer entity);
        void DeleteOfferStudiegebied(StudiegebiedOffer entity);
        void DeleteOfferOpleiding(OpleidingOffer entity);
        IEnumerable<Type> GetAllTypes();
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
            var test = DateTime.Now;
             var result = _db.Offers.Where(o => o.Company.Verified == true && o.ExpirationDate > DateTime.Now).ToList();
            var test2 = result.First();
            var test3 = test2.ExpirationDate;
            var test4 = test - test3;
            var test5 = test3 - test;
            var test6 = (test3 > test);
            return result;
        }

        public IEnumerable<Offers> GetAllOffersByCompany(int id)
        {
            return _db.Offers.Where(o => o.CompanyId == id).ToList();
        }

        public Offers GetOfferById(int id)
        {
            return _db.Offers.Where(o => o.Id == id).Include(o => o.StudiegebiedOffer).Include(o => o.OpleidingOffer).FirstOrDefault();
        }

        public void UpdateOffer(Offers offer)
        {
            _db.Entry(offer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Offers> GetOffersByStudiegebiedId(string id)
        {
            var studiegebiedOffer = _db.StudiegebiedOffer.Where(o => o.IdStudiegebied == id).Include(o => o.IdOfferNavigation).ToList();
            List<Offers> offers = new List<Offers>();
            foreach (var offer in studiegebiedOffer)
            {
                if (offer.IdOfferNavigation.Verified && offer.IdOfferNavigation.ExpirationDate > DateTime.Now)
                {
                    offers.Add(offer.IdOfferNavigation);
                }
            }
            return offers;
        }
        public IEnumerable<Offers> GetOffersByOpleidingId(string id)
        {
            var opleidingOffer = _db.OpleidingOffer.Where(o => o.IdOpleiding == id).Include(o => o.IdOfferNavigation).ToList();
            List<Offers> offers = new List<Offers>();
            foreach (var offer in opleidingOffer)
            {
                if (offer.IdOfferNavigation.Verified && offer.IdOfferNavigation.ExpirationDate > DateTime.Now)
                {
                    offers.Add(offer.IdOfferNavigation);
                }
            }
            return offers;
        }
        public IEnumerable<Offers> GetOffersByAfstudeerrichtingId(string id)
        {
            var afstudeerrichtingOffer = _db.AfstudeerrichtingOffer.Where(o => o.IdAfstudeerrichting == id).Include(o => o.IdOfferNavigation).ToList();
            List<Offers> offers = new List<Offers>();
            foreach (var offer in afstudeerrichtingOffer)
            {
                if (offer.IdOfferNavigation.Verified && offer.IdOfferNavigation.ExpirationDate > DateTime.Now)
                {
                    offers.Add(offer.IdOfferNavigation);
                }
            }
            return offers;
        }
        public IEnumerable<Offers> GetOffersByKeuzeId(string id)
        {
            var keuzeOffer = _db.KeuzeOffer.Where(o => o.IdKeuze == id).Include(o => o.IdOfferNavigation).ToList();
            List<Offers> offers = new List<Offers>();
            foreach (var offer in keuzeOffer)
            {
                if (offer.IdOfferNavigation.Verified && offer.IdOfferNavigation.ExpirationDate > DateTime.Now)
                {
                    offers.Add(offer.IdOfferNavigation);
                }
            }
            return offers;
        }

        public IEnumerable<Offers> GetOffersByType(Type type)
        {
            return _db.Offers.Where(o => o.Type == type).ToList();
        }

        public void CreateOfferStudiegebied(StudiegebiedOffer entity)
        {
           
            _db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _db.SaveChanges();
        }

        public void CreateOfferOpleiding(OpleidingOffer entity)
        {
            _db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _db.SaveChanges();
        }

        public void DeleteOfferStudiegebied(StudiegebiedOffer entity)
        {
            var os = _db.StudiegebiedOffer.Where(s => s.IdOffer == entity.IdOffer && s.IdStudiegebied == entity.IdStudiegebied).FirstOrDefault();
            //var offer = _db.Offers.Where(o => o.Id == id).FirstOrDefault();
            _db.StudiegebiedOffer.Remove(os);
            _db.SaveChanges();
        }

        public void DeleteOfferOpleiding(OpleidingOffer entity)
        {
            var oo = _db.OpleidingOffer.Where(o => o.IdOffer == entity.IdOffer && o.IdOpleiding == entity.IdOpleiding).FirstOrDefault();
            //var offer = _db.Offers.Where(o => o.Id == id).FirstOrDefault();
            _db.OpleidingOffer.Remove(oo);
            _db.SaveChanges();
        }

        public IEnumerable<Type> GetAllTypes()
        {
            return _db.Type.ToList();
        }
    }
}
