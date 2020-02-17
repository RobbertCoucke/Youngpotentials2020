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
            throw new NotImplementedException();
        }

        public IEnumerable<Offers> GetOffersByTags(IList<string> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Offers> GetOffersByTypes(IList<string> types)
        {
            throw new NotImplementedException();
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
