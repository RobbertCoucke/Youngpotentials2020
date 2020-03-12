using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youngpotentials.DAO;
using Youngpotentials.Domain.Entities;
using Youngpotentials.Domain.Models.Requests;
using Youngpotentials.Domain.Models.Responses;
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
        IEnumerable<TypeResponse> GetAllTypes();
        void DeleteAllStudieConnectionsFromOfferId(int id);
        void AddTagsToOffer(IList<Studiegebied> tags, int offerId);
    }
    public class OfferService : IOfferService
    {

        private IOfferDAO _offerDAO;
        private IStudiegebiedDAO _studiegebiedDAO;
        private IOpleidingDAO _opleidingDAO;
        
        public OfferService(IOfferDAO offerDAO, IStudiegebiedDAO studiegebiedDAO, IOpleidingDAO opleidingDAO)
        {
            _offerDAO = offerDAO;
            _studiegebiedDAO = studiegebiedDAO;
            _opleidingDAO = opleidingDAO;
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

            //check if tags have to be added or removed
            var list = new List<Studiegebied>();
            foreach(var studiegebiedOffer in oldOffer.StudiegebiedOffer)
            {
                list.Add(new Studiegebied { Id = studiegebiedOffer.IdStudiegebied });
            }

            foreach(var opleidingOffer in oldOffer.OpleidingOffer)
            {
                var op = _opleidingDAO.GetById(opleidingOffer.IdOpleiding);
                foreach(var s in list.Where(s => s.Id == op.IdStudiegebied))
                {
                    s.Opleiding.Add(op);
                }
            }

            var toAdd = offer.Tags.Except(list).ToList();
            var toDelete = list.Except(offer.Tags).ToList();
            if(toAdd.Count > 0)
            {
                AddTagsToOffer(toAdd, id);
            }
            if(toDelete.Count > 0)
            {
                removeTagsFromOffer(toDelete, id);
            }
            


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

        //gets all offers from studiegebiedId/opleidingId/afstudeerrichtingId/keuzeId
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

        //gets all offers with corresponding tags (studiegebied, opleiding, afstudeerrichting, keuze)
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


        //converts studiegebiedArray to idArray
        public IList<string> GetStudiegebiedToTags(IList<Studiegebied> studiegebied)
        {
            List<string> ids = new List<string>();
            foreach (var s in studiegebied)
            {

                    ids.Add(s.Id);

                    foreach (var o in s.Opleiding)
                    {

                            ids.Add(o.Id);
  
                            foreach (var a in o.Afstudeerrichting)
                            {

                                    ids.Add(a.Id);
                     
                                    foreach (var k in a.Keuze)
                                    {
                                        ids.Add(k.Id);
                                    }
                                

                            }
                        
                    }
                
            }
            return ids;
        }
        //public IList<string> GetStudiegebiedToTags(IList<Studiegebied> studiegebied)
        //{
        //    List<string> ids = new List<string>();
        //    foreach (var s in studiegebied)
        //    {
        //        if (s.Opleiding.Count == 0)
        //        {
        //            ids.Add(s.Id);
        //        }
        //        else
        //        {
        //            foreach (var o in s.Opleiding)
        //            {
        //                if (o.Afstudeerrichting.Count == 0)
        //                {
        //                    ids.Add(o.Id);
        //                }
        //                else
        //                {
        //                    foreach (var a in o.Afstudeerrichting)
        //                    {
        //                        if (a.Keuze.Count == 0)
        //                        {
        //                            ids.Add(a.Id);
        //                        }
        //                        else
        //                        {
        //                            foreach (var k in a.Keuze)
        //                            {
        //                                ids.Add(k.Id);
        //                            }
        //                        }

        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return ids;
        //}


            //gets all offers with corresponding type and tags
        public IEnumerable<Offers> GetOffersByTypesAndTags(IList<Type> types, IList<Studiegebied> ids)
        {
            try
            {
                HashSet<Offers> hashOffers = new HashSet<Offers>();
                hashOffers.UnionWith(GetOffersByTypes(types));
                if (types.Count > 0 && ids.Count > 0)
                {

                    hashOffers.IntersectWith(GetOffersByTags(ids));
                }
                else
                {
                    hashOffers.UnionWith(GetOffersByTags(ids));
                }

                return hashOffers;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        //gets all offers with corresponding types
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

        //couples studiegebied and opleidingtags to offer
        public void AddTagsToOffer(IList<Studiegebied> tags, int offerId)
        {
            foreach(var studiegebied in tags)
            {
                var st = _studiegebiedDAO.GetById(studiegebied.Id);
                _offerDAO.CreateOfferStudiegebied(new StudiegebiedOffer { IdStudiegebied = st.Id, IdOffer = offerId });
                foreach(var opleiding in studiegebied.Opleiding)
                {
                    var op = _opleidingDAO.GetById(opleiding.Id);
                    _offerDAO.CreateOfferOpleiding(new OpleidingOffer { IdOffer = offerId, IdOpleiding = op.Id });
                }
            }
        }

        //decouples studiegebied and opleidingtags from offer
        public void removeTagsFromOffer(IList<Studiegebied> tags, int offerId)
        {
            foreach (var studiegebied in tags)
            {

                var st = _studiegebiedDAO.GetById(studiegebied.Id);
                _offerDAO.DeleteOfferStudiegebied(new StudiegebiedOffer { IdStudiegebied = st.Id, IdOffer = offerId });
                foreach (var opleiding in studiegebied.Opleiding)
                {
                    var op = _opleidingDAO.GetById(opleiding.Id);
                    _offerDAO.DeleteOfferOpleiding(new OpleidingOffer { IdOffer = offerId, IdOpleiding = op.Id });
                }
            }
        }

        public IEnumerable<TypeResponse> GetAllTypes()
        {
            return _offerDAO.GetAllTypes();
        }


        //decouples all tags from offer
        public void DeleteAllStudieConnectionsFromOfferId(int id)
        {
            var studiegebieden = _offerDAO.GetStudiegebiedOffersFromOfferId(id);
            var opleidingen = _offerDAO.GetOpleidingOffersFromOfferId(id);

            foreach(var s in studiegebieden)
            {
                _offerDAO.DeleteOfferStudiegebied(s);
            }

            foreach(var o in opleidingen)
            {
                _offerDAO.DeleteOfferOpleiding(o);
            }
        }
    }
}
