using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.DAO;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.Service
{
    public interface IFavoritesService
    {
        IEnumerable<Favorites> GetAllFavoritesFromUserId(int id);
        Favorites AddFavorite(int userId, int offerId);
        void DeleteFavorite(int id);
    }
    public class FavoritesService : IFavoritesService
    {

        private IFavoritesDAO _favoritesDAO;

        public FavoritesService(IFavoritesDAO favoriteDAO)
        {
            _favoritesDAO = favoriteDAO;
        }

        public Favorites AddFavorite(int userId, int offerId)
        {
            return _favoritesDAO.AddFavorite(userId, offerId);
        }

        public void DeleteFavorite(int id)
        {
            _favoritesDAO.DeleteFavorite(id);
        }

        public IEnumerable<Favorites> GetAllFavoritesFromUserId(int id)
        {
            return _favoritesDAO.GetAllFavoritesFromUserId(id);
        }
    }
}
