using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.DAO
{
    public interface IKeuzeDAO
    {
        IEnumerable<Keuze> GetAll();
        Keuze GetById(string id);
        IEnumerable<Keuze> GetAllByAfstudeerrichting(string id);
        void DeleteById(string id);
        void Update(Keuze keuze);
        Keuze CreateKeuze(Keuze keuze);
    }
    public class KeuzeDAO : IKeuzeDAO
    {
        private YoungpotentialsV1Context _db;

        public KeuzeDAO()
        {
            _db = new YoungpotentialsV1Context();
        }

        public Keuze CreateKeuze(Keuze keuze)
        {
            try
            {
                _db.Entry(keuze).State = EntityState.Added;
                _db.SaveChanges();
                return keuze;
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
                var keuze = _db.Keuze.FirstOrDefault(k => k.Id == id);
                _db.Keuze.Remove(keuze);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public IEnumerable<Keuze> GetAll()
        {
            try
            {
                return _db.Keuze.Include(a => a.Afstudeerrichting)
                                .Include(k => k.KeuzeOffer).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public IEnumerable<Keuze> GetAllByAfstudeerrichting(string id)
        {
            try
            {
                return _db.Keuze.Where(k => k.AfstudeerrichtingId == id).Include(a => a.Afstudeerrichting)
                    .Include(k => k.KeuzeOffer).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public Keuze GetById(string id)
        {
            try
            {
                return _db.Keuze.Where(k => k.Id == id).Include(a => a.Afstudeerrichting)
                                .Include(k => k.KeuzeOffer).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void Update(Keuze keuze)
        {
            try
            {
                _db.Entry(keuze).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
