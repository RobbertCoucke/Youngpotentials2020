using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youngpotentials.Domain.Entities;
using Youngpotentials.Domain.Models.Responses;
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
        IEnumerable<TypeResponse> GetAllTypes();
        IEnumerable<StudiegebiedOffer> GetStudiegebiedOffersFromOfferId(int id);
        IEnumerable<OpleidingOffer> GetOpleidingOffersFromOfferId(int id);
    }
    public class OfferDAO : IOfferDAO
    {

        private YoungpotentialsV1Context _db;
        private ICompanyDAO _companyDAO;

        public OfferDAO(ICompanyDAO companyDAO)
        {
            _db = new YoungpotentialsV1Context();
            _companyDAO = companyDAO;
        }

        public Offers CreateOffer(Offers offer)
        {
            _db.Entry(offer).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _db.SaveChanges();
            _db.DetachAllEntities();
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
            try
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
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        //gets all offers by companyId
        public IEnumerable<Offers> GetAllOffersByCompany(int id)
        {
            try
            {
                return _db.Offers.Where(o => o.CompanyId == id).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        public Offers GetOfferById(int id)
        {
            try
            {
                return _db.Offers.Where(o => o.Id == id).Include(o => o.StudiegebiedOffer).Include(o => o.OpleidingOffer).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void UpdateOffer(Offers offer)
        {
            try
            {
                _db.Entry(offer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //gets all offers by studiegebiedId   (gets all offers with tag of studiegebied)
        public IEnumerable<Offers> GetOffersByStudiegebiedId(string id)
        {
            try
            {
                var studiegebiedOffer = _db.StudiegebiedOffer.Where(o => o.IdStudiegebied == id).Include(o => o.IdOfferNavigation).ToList();
            
                List<Offers> offers = new List<Offers>();
                foreach (var offer in studiegebiedOffer)
                {
                    var company = _companyDAO.GetCompanyById((int)offer.IdOfferNavigation.CompanyId);
                    if ((bool) company.Verified && offer.IdOfferNavigation.ExpirationDate > DateTime.Now)
                    {
                        offers.Add(offer.IdOfferNavigation);
                    }
                }
                return offers; 
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        //gets all offers by opleidingId   (gets all offers with tag of opleiding)
        public IEnumerable<Offers> GetOffersByOpleidingId(string id)
        {
            try
            {
                var opleidingOffer = _db.OpleidingOffer.Where(o => o.IdOpleiding == id).Include(o => o.IdOfferNavigation).ToList();
                List<Offers> offers = new List<Offers>();
                foreach (var offer in opleidingOffer)
                {
                    var company = _companyDAO.GetCompanyById((int)offer.IdOfferNavigation.CompanyId);
                    if ((bool)company.Verified && offer.IdOfferNavigation.ExpirationDate > DateTime.Now)
                    {
                        offers.Add(offer.IdOfferNavigation);
                    }
                }
                return offers;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        //gets all offers by afstudeerrichtingId   (gets all offers with tag of afstudeerrichting)
        public IEnumerable<Offers> GetOffersByAfstudeerrichtingId(string id)
        {
            try
            {
                var afstudeerrichtingOffer = _db.AfstudeerrichtingOffer.Where(o => o.IdAfstudeerrichting == id).Include(o => o.IdOfferNavigation).ToList();
                List<Offers> offers = new List<Offers>();
                foreach (var offer in afstudeerrichtingOffer)
                {
                    var company = _companyDAO.GetCompanyById((int)offer.IdOfferNavigation.CompanyId);
                    if ((bool)company.Verified && offer.IdOfferNavigation.ExpirationDate > DateTime.Now)
                    {
                        offers.Add(offer.IdOfferNavigation);
                    }
                }
                return offers;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        //gets all offers by keuzeId   (gets all offers with tag of keuze)
        public IEnumerable<Offers> GetOffersByKeuzeId(string id)
        {
            try
            {
                var keuzeOffer = _db.KeuzeOffer.Where(o => o.IdKeuze == id).Include(o => o.IdOfferNavigation).ToList();
                List<Offers> offers = new List<Offers>();
                foreach (var offer in keuzeOffer)
                {
                    var company = _companyDAO.GetCompanyById((int)offer.IdOfferNavigation.CompanyId);
                    if ((bool)company.Verified && offer.IdOfferNavigation.ExpirationDate > DateTime.Now)
                    {
                        offers.Add(offer.IdOfferNavigation);
                    }
                }
                return offers;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //gets all offers by type
        public IEnumerable<Offers> GetOffersByType(Type type)
        {
            try
            {
                return _db.Offers.Where(o => o.Type == type && (bool) o.Company.Verified && o.ExpirationDate > DateTime.Now).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void CreateOfferStudiegebied(StudiegebiedOffer entity)
        {
            try
            {
                _db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void CreateOfferOpleiding(OpleidingOffer entity)
        {
            try
            {
                _db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void DeleteOfferStudiegebied(StudiegebiedOffer entity)
        {
            try
            {
                var os = _db.StudiegebiedOffer.Where(s => s.IdOffer == entity.IdOffer && s.IdStudiegebied == entity.IdStudiegebied).FirstOrDefault();
                //var offer = _db.Offers.Where(o => o.Id == id).FirstOrDefault();
                _db.StudiegebiedOffer.Remove(os);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void DeleteOfferOpleiding(OpleidingOffer entity)
        {
            try
            {
                var oo = _db.OpleidingOffer.Where(o => o.IdOffer == entity.IdOffer && o.IdOpleiding == entity.IdOpleiding).FirstOrDefault();
                //var offer = _db.Offers.Where(o => o.Id == id).FirstOrDefault();
                _db.OpleidingOffer.Remove(oo);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        //returns all types
        public IEnumerable<TypeResponse> GetAllTypes()
        {
            try
            {
                //return _db.Type.Select( t=> t).ToList();
                //return from t in _db.Type select t;
                //return _db.Type.ToList();
                var test = _db.Type.Select(p => new TypeResponse { Id = p.Id, Name = p.Name }).ToList();
                return test;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //gets al StudiegebiedOffers from offerId
        public IEnumerable<StudiegebiedOffer> GetStudiegebiedOffersFromOfferId(int id)
        {
            try
            {
                return _db.StudiegebiedOffer.Where(s => s.IdOffer == id).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //gets al opleidingsOffers from offerId
        public IEnumerable<OpleidingOffer> GetOpleidingOffersFromOfferId(int id)
        {
            try
            {
                return _db.OpleidingOffer.Where(o => o.IdOffer == id).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
