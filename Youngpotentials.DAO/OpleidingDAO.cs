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
        private YoungpotentialsV1Context _db;

        public OpleidingDAO()
        {
            _db = new YoungpotentialsV1Context();
        }
        public Opleiding CreateOpleiding(Opleiding opleiding)
        {
            try
            {
                _db.Entry(opleiding).State = EntityState.Added;
                _db.SaveChanges();
                return opleiding;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void DeleteById(string id)
        {
            try
            {
                var opleiding = _db.Opleiding.FirstOrDefault(o => o.Id == id);
                _db.Opleiding.Remove(opleiding);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public IEnumerable<Opleiding> GetAll()
        {
            try
            {
                return _db.Opleiding.Include(s => s.IdStudiegebiedNavigation)
                                    .Include(a => a.Afstudeerrichting)
                                    .Include(o => o.OpleidingOffer)
                                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public Opleiding GetById(string id)
        {
            try
            {
                return _db.Opleiding.Where(op => op.Id == id).Include(s => s.IdStudiegebiedNavigation)
                                    .Include(a => a.Afstudeerrichting)
                                    .Include(o => o.OpleidingOffer).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public IEnumerable<Opleiding> GetAllByStudiegebied(string id)
        {
            try
            {
                return _db.Opleiding.Where(op => op.IdStudiegebied == id).Include(s => s.IdStudiegebiedNavigation)
                        .Include(a => a.Afstudeerrichting)
                        .Include(o => o.OpleidingOffer).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void Update(Opleiding opleiding)
        {
            try
            {
                _db.Entry(opleiding).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
