using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.DAO;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.Service
{
    public interface IOfferService
    {
        IEnumerable<Offers> GetAllOffers();
        IEnumerable<Offers> GetAllOffersByCompany(int id);
        Offers GetOffersById(int id);
        Offers CreateOffer(Offers offer);
        void UpdateOffer(Offers offer);
        void DeleteOffer(int id);
        IEnumerable<Offers> GetOffersByTagId(string id);
        IEnumerable<Offers> GetOffersByTags(IList<string> ids);
        IEnumerable<Offers> GetOffersByTypes(IList<string> types);
        IEnumerable<Offers> GetOffersByTypesAndTags(IList<string> ids, IList<string> types);
    }
    public class OfferService : IOfferService
    {

        private IOfferDAO _offerDAO;
        
        public OfferService(IOfferDAO offerDAO)
        {
            _offerDAO = offerDAO;
        }
        public Offers CreateOffer(Offers offer)
        {
            throw new NotImplementedException();
        }

        public void DeleteOffer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Offers> GetAllOffers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Offers> GetAllOffersByCompany(int id)
        {
            throw new NotImplementedException();
        }

        public Offers GetOffersById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Offers> GetOffersByTagId(string id)
        {
            return _offerDAO.GetOffersByTags(id);
        }

        public IEnumerable<Offers> GetOffersByTagId(string id)
        {
            //Misschien substring van (0,1)
            string substring = id.Substring(0);
            switch (substring)
            {
                case "s":
                    Console.WriteLine("Studiegebied" + id);
                    GetOffersByStudiegebiedId(id);
                case "o":
                    Console.WriteLine("Opleiding" + id);
                    GetOffersByOpleidingId(id);
                case "a":
                    Console.WriteLine("Afstudeerrichting" + id);
                    GetOffersByAfstudeerrichtingId(id);
                case "k":
                    Console.WriteLine("Keuze" + id);
                    GetOffersByKeuzeId(id);
                default:
                    Console.WriteLine("Houston, we have a problem");
                    return null;
            }
        }

        public IEnumerable<Offers> GetOffersByStudiegebiedId(string id)
        {
            return _offerDAO.GetOffersByStudiegebiedId(id);
        }
        public IEnumerable<Offers> GetOffersByOpleidingId(string id)
        {
            return _offerDAO.GetOffersByOpleidingId(id);
        }
        public IEnumerable<Offers> GetOffersByAfstudeerrichtingId(string id)
        {
            return _offerDAO.GetOffersByAfstudeerrichtingId(id);
        }
        public IEnumerable<Offers> GetOffersByKeuzeId(string id)
        {
            return _offerDAO.GetOffersByKeuzeId(id);
        }

        public IEnumerable<Offers> GetOffersByTags(IList<string> ids)
        {
            foreach (string id in ids)
            {
                
            }
        }

        public IEnumerable<Offers> GetOffersByTypesAndTags(IList<string> ids, IList<string> types)
        {
            throw new NotImplementedException();
        }

        public void UpdateOffer(Offers offer)
        {
            throw new NotImplementedException();
        }
    }
}
