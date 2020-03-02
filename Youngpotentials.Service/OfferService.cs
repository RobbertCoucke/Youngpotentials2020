using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.DAO;
using Youngpotentials.Domain.Entities;
using Youngpotentials.Domain.Models.Requests;
using Type = Youngpotentials.Domain.Entities.Type;

namespace Youngpotentials.Service
{
    public interface IOfferService
    {
        IEnumerable<Offers> GetAllOffers();
        IEnumerable<Offers> GetAllOffersByCompany(int id);
        Offers GetOfferById(int id);
        Offers CreateOffer(Offers offer);
        void UpdateOffer(UpdateOfferRequest offer, int id);
        void DeleteOffer(int id);
        IEnumerable<Offers> GetOffersByTagId(string id);
        IEnumerable<Offers> GetOffersByTags(IList<Studiegebied> studiegebied);
        IList<string> GetStudiegebiedToTags(IList<Studiegebied> studiegebied);
        IEnumerable<Offers> GetOffersByTypes(IList<Type> types);
        IEnumerable<Offers> GetOffersByTypesAndTags(IList<Type> types, IList<Studiegebied> ids);
        IEnumerable<Offers> GetOffersByType(Type type);
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
            //TODO: Check als er sommige velden leeg zijn en dit dan overnemen van de company.
            return _offerDAO.CreateOffer(offer);
        }

        public void DeleteOffer(int id)
        {
            _offerDAO.DeleteOffer(id);
        }
        public void UpdateOffer(UpdateOfferRequest offer, int id)
        {
            //Haalt de huidige vacature info op uit de database
            var oldOffer = GetOfferById(id);

            /*Checkt indien de nieuwe vacature informatie leeg is, 
             *indien leeg moet de huidige vacature info in de nieuwe geplaatst worden.*/
            if (offer.Title != null)
            {
                oldOffer.Title = offer.Title;
            }

            if (offer.Description != null)
            {
                oldOffer.Description = offer.Description;
            }

            if (offer.Email != null)
            {
                oldOffer.Email = offer.Email;
            }

            if (offer.Name != null)
            {
                oldOffer.Name = offer.Name;
            }

            if (offer.Address != null)
            {
                oldOffer.Address = offer.Address;
            }

            if (offer.TypeId != null)
            {
                oldOffer.TypeId = (int) offer.TypeId;
            }

            if (offer.ZipCode != null)
            {
                oldOffer.ZipCode = offer.ZipCode;
            }

            if (offer.City != null)
            {
                oldOffer.City = offer.City;
            }

            if (offer.Country != null)
            {
                oldOffer.Country = offer.Country;
            }

            //TODO: AttachementID toevoegen hier, UpdateOfferRequest en CreateOfferRequest

            _offerDAO.UpdateOffer(oldOffer);
        }
        public IEnumerable<Offers> GetAllOffers()
        {
            return _offerDAO.GetAllOffers();
        }
        public IEnumerable<Offers> GetAllOffersByCompany(int id)
        {
            return _offerDAO.GetAllOffersByCompany(id);
        }
        public Offers GetOfferById(int id)
        {
            return _offerDAO.GetOfferById(id);
        }
        public IEnumerable<Offers> GetOffersByTagId(string id)
        {
            //Misschien substring van (0,1)
            string substring = id.Substring(0,1);
            switch (substring)
            {
                case "s":
                    Console.WriteLine("Studiegebied" + id);
                    return GetOffersByStudiegebiedId(id);
                case "o":
                    Console.WriteLine("Opleiding" + id);
                    return GetOffersByOpleidingId(id);
                case "a":
                    Console.WriteLine("Afstudeerrichting" + id);
                    return GetOffersByAfstudeerrichtingId(id);
                case "k":
                    Console.WriteLine("Keuze" + id);
                    return GetOffersByKeuzeId(id);
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
        public IEnumerable<Offers> GetOffersByTags(IList<Studiegebied> studiegebied)
        {
            HashSet<Offers> hashOffers = new HashSet<Offers>();
            if (studiegebied != null)
            {
                IList<string> ids = GetStudiegebiedToTags(studiegebied);
                foreach (var id in ids)
                {
                    var offers = GetOffersByTagId(id);
                    hashOffers.UnionWith(offers);
                    //foreach (var offer in offers)
                    //{
                    //    hashOffers.Add(offer);
                    //}
                }
            }

            return hashOffers;         
        }
        public IList<string> GetStudiegebiedToTags(IList<Studiegebied> studiegebied)
        {
            List<string> ids = new List<string>();
            foreach (var s in studiegebied)
            {
                if (s.Opleiding.Count == 0)
                {
                    ids.Add(s.Id);
                }
                else
                {
                    foreach (var o in s.Opleiding)
                    {
                        if (o.Afstudeerrichting.Count == 0)
                        {
                            ids.Add(o.Id);
                        }
                        else
                        {
                            foreach (var a in o.Afstudeerrichting)
                            {
                                if (a.Keuze.Count == 0)
                                {
                                    ids.Add(a.Id);
                                }
                                else
                                {
                                    foreach (var k in a.Keuze)
                                    {
                                        ids.Add(k.Id);
                                    }
                                }

                            }
                        }
                    }
                }
            }
            return ids;
        }

        public IEnumerable<Offers> GetOffersByTypesAndTags(IList<Type> types, IList<Studiegebied> ids)
        {
            HashSet<Offers> hashOffers = new HashSet<Offers>();
            hashOffers.UnionWith(GetOffersByTypes(types));
            hashOffers.IntersectWith(GetOffersByTags(ids));

            return hashOffers;
        }

        public IEnumerable<Offers> GetOffersByTypes(IList<Type> types)
        {
            HashSet<Offers> hashOffers = new HashSet<Offers>();
            if (types != null)
            { 
                foreach(var type in types)
                {
                    hashOffers.UnionWith(GetOffersByType(type));
                }
            }
            
            return hashOffers;
        }

        public IEnumerable<Offers> GetOffersByType(Type type)
        {

            return _offerDAO.GetOffersByType(type);
        }
    }
}
