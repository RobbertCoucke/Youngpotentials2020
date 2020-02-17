using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.DAO
{
    public interface IOpleidingDAO
    {
        IEnumerable<Opleiding> GetAll();
        Opleiding GetById(string id);
        IEnumerable<Opleiding> GetAllByStudiegebied(string id);
        void DeleteById(string id);
        void Update(Opleiding opleiding);
        Opleiding CreateOpleiding(Opleiding opleiding);
    }
    public class OpleidingDAO : IOpleidingDAO
    {
        private YoungpotentialsContext _db;

        public OpleidingDAO()
        {
            _db = new YoungpotentialsContext();
        }
        public Opleiding CreateOpleiding(Opleiding opleiding)
        {
            _db.Entry(opleiding).State = EntityState.Added;
            _db.SaveChanges();
            return opleiding;
        }

        public void DeleteById(string id)
        {
            var opleiding = _db.Opleiding.FirstOrDefault(o => o.Id == id);
            _db.Opleiding.Remove(opleiding);
            _db.SaveChanges();
        }

        public IEnumerable<Opleiding> GetAll()
        {
            return _db.Opleiding.Include(s => s.IdStudiegebiedNavigation)
                                .Include(a => a.Afstudeerrichting)
                                .Include(o => o.OpleidingOffer)
                                .ToList();
        }

        public Opleiding GetById(string id)
        {
            return _db.Opleiding.Where(op => op.Id == id).Include(s => s.IdStudiegebiedNavigation)
                                .Include(a => a.Afstudeerrichting)
                                .Include(o => o.OpleidingOffer).FirstOrDefault();
        }

        public IEnumerable<Opleiding> GetAllByStudiegebied(string id)
        {
            return _db.Opleiding.Where(op => op.IdStudiegebied == id).Include(s => s.IdStudiegebiedNavigation)
                    .Include(a => a.Afstudeerrichting)
                    .Include(o => o.OpleidingOffer).ToList();
        }

        public void Update(Opleiding opleiding)
        {
            _db.Entry(opleiding).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
