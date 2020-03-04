using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.DAO
{
    public interface IFavoritesDAO
    {
        IEnumerable<Favorites> GetAllFavoritesFromUserId(int id);
        Favorites AddFavorite(int userId, int offerId);
        void DeleteFavorite(int id);
    }
    public class FavoritesDAO : IFavoritesDAO
    {

        private YoungpotentialsContext _db;

        public FavoritesDAO()
        {
            _db = new YoungpotentialsContext();
        }
        public Favorites AddFavorite(int userId, int offerId)
        {
            var favorite = new Favorites();
            favorite.StudentId = userId;
            favorite.OfferId = offerId;
            _db.Entry(favorite).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _db.SaveChanges();
            return favorite;
        }

        public void DeleteFavorite(int id)
        {
            var favorite =_db.Favorites.Where(f => f.Id == id).FirstOrDefault();
            if(favorite != null)
            {
                _db.Favorites.Remove(favorite);
            }
            _db.SaveChanges();
        }

        public IEnumerable<Favorites> GetAllFavoritesFromUserId(int id)
        {
            return _db.Favorites.Include(f => f.Offer).ToList();
        }
    }
}
