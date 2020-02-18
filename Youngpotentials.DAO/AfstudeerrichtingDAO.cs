using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.DAO
{
    public interface IAfstudeerrichtingDAO
    {
        IEnumerable<Afstudeerrichting> GetAll();
        Afstudeerrichting GetById(string id);
        IEnumerable<Afstudeerrichting> GetAllByOpleiding(string id);
        void DeleteById(string id);
        void Update(Afstudeerrichting afstudeerrichting);
        Afstudeerrichting CreateAfstudeerrichting(Afstudeerrichting afstudeerrichting);
    }
    public class AfstudeerrichtingDAO : IAfstudeerrichtingDAO
    {
        private YoungpotentialsContext _db;

        public AfstudeerrichtingDAO()
        {
            _db = new YoungpotentialsContext();
        }
        public Afstudeerrichting CreateAfstudeerrichting(Afstudeerrichting afstudeerrichting)
        {
            _db.Entry(afstudeerrichting).State = EntityState.Added;
            _db.SaveChanges();
            return afstudeerrichting;
        }

        public void DeleteById(string id)
        {
            var afstudeerrichting = _db.Afstudeerrichting.FirstOrDefault(a => a.Id == id);
            _db.Afstudeerrichting.Remove(afstudeerrichting);
            _db.SaveChanges();
        }

        public IEnumerable<Afstudeerrichting> GetAll()
        {
            return _db.Afstudeerrichting.Include(o => o.Opleiding)
                                        .Include(a => a.AfstudeerrichtingOffer)
                                        .Include(k => k.Keuze)
                                        .ToList();
        }

        public Afstudeerrichting GetById(string id)
        {
            return _db.Afstudeerrichting.Where(a => a.Id == id).Include(o => o.Opleiding)
                                                               .Include(a => a.AfstudeerrichtingOffer)
                                                               .Include(k => k.Keuze)
                                                               .FirstOrDefault();
        }

        public IEnumerable<Afstudeerrichting> GetAllByOpleiding(string id)
        {
            return _db.Afstudeerrichting.Where(a => a.OpleidingId == id).Include(o => o.Opleiding)
                                                   .Include(a => a.AfstudeerrichtingOffer)
                                                   .Include(k => k.Keuze).ToList();
        }

        public void Update(Afstudeerrichting afstudeerrichting)
        {
            _db.Entry(afstudeerrichting).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
