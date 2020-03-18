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
            return _offerDAO.CreateOffer(offer);
        }

        public void DeleteOffer(int id)
        {
            _offerDAO.DeleteOffer(id);
        }
        public void UpdateOffer(UpdateOfferRequest offer, int id)
        {
            ///Retrieves the current offer info from the database
            var oldOffer = GetOfferById(id);

            ///check if tags have to be added or removed
            var list = new List<Studiegebied>();
            foreach (var studiegebiedOffer in oldOffer.StudiegebiedOffer)
            {
                list.Add(new Studiegebied { Id = studiegebiedOffer.IdStudiegebied });
            }

            foreach (var opleidingOffer in oldOffer.OpleidingOffer)
            {
                var op = _opleidingDAO.GetById(opleidingOffer.IdOpleiding);
                foreach (var s in list.Where(s => s.Id == op.IdStudiegebied))
                {
                    s.Opleiding.Add(op);
                }
            }


            //voegt toe of verwijdert tags volgens updatemodel
            var toAdd = offer.Tags.Except(list).ToList();
            var toDelete = list.Except(offer.Tags).ToList();
            if (toAdd.Count > 0)
            {
                AddTagsToOffer(toAdd, id);
            }
            else
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
                oldOffer.TypeId = (int)offer.TypeId;
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

        /// <summary>
        /// Gets all the offers by 1 TagID (=studiegebiedId/opleidingId/afstudeerrichtingId/keuzeId)
        /// afstudeerrichttingId and keuzeId are not used anymore
        /// </summary>
        /// <param name="id">a tagId</param>
        /// <returns>Offers</returns>
        public IEnumerable<Offers> GetOffersByTagId(string id)
        {
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

        /// <summary>
        /// gets all offers with corresponding tags (studiegebied, opleiding, afstudeerrichting, keuze)
        /// </summary>
        /// <param name="studiegebied"></param>
        /// <returns></returns>
        //
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
                }
            }
            return hashOffers;         
        }

        /// <summary>
        /// Converts a list of studiegebieden to a list of strings
        /// </summary>
        /// <param name="studiegebied">A list with studiegebieden</param>
        /// <returns>A list with the ids of studiegebieden</returns>
        public IList<string> GetStudiegebiedToTags(IList<Studiegebied> studiegebied)
        {
            List<string> ids = new List<string>();
            foreach (var s in studiegebied)
            {

               if(s.Opleiding.Count == 0)
                {
                    ids.Add(s.Id);
                }
                    
                    foreach (var o in s.Opleiding)
                    {

                            ids.Add(o.Id);
                        
                    }
                
            }
            return ids;
        }

        /// <summary>
        /// Gets all offers with corresponding types and tags
        /// </summary>
        /// <param name="types">A list with types</param>
        /// <param name="ids">A list with studiegebieden</param>
        /// <returns>A hashset with offers</returns>
        public IEnumerable<Offers> GetOffersByTypesAndTags(IList<Type> types, IList<Studiegebied> ids)
        {
            try
            {
                HashSet<Offers> hashOffers = new HashSet<Offers>();
                hashOffers.UnionWith(GetOffersByTypes(types));

                if (types.Count > 0 && ids.Count > 0)
                {
                    //Get only the offers that are present in both Lists
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

        /// <summary>
        /// Gets all offers with corresponding types
        /// </summary>
        /// <param name="types">List of types</param>
        /// <returns>Offers with the type equal to a type present in types</returns>
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

        /// <summary>
        /// Gets all offers with corresponding type
        /// </summary>
        /// <param name="type">a Type</param>
        /// <returns>Offers with the type equal to the param type</returns>
        public IEnumerable<Offers> GetOffersByType(Type type)
        {

            return _offerDAO.GetOffersByType(type);
        }

        /// <summary>
        /// Couples studiegebied and opleidingtags to offer
        /// </summary>
        /// <param name="tags">tags that needs to be coupled</param>
        /// <param name="offerId">offerId where tags should be coupled to</param>
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
        /// <summary>
        /// Decouples studiegebied and opleidingtags from offer
        /// </summary>
        /// <param name="tags">tags that needs to be decoupled from offer</param>
        /// <param name="offerId">offerId where tags should be decoupled</param>
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

        /// <summary>
        /// Decouples all tags from offer
        /// </summary>
        /// <param name="offerId">offerId where the tags needs to be decoupled</param>
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
