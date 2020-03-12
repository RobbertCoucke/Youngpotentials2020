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
            _db.Entry(keuze).State = EntityState.Added;
            _db.SaveChanges();
            return keuze;
        }

        public void DeleteById(string id)
        {
            var keuze = _db.Keuze.FirstOrDefault(k => k.Id == id);
            _db.Keuze.Remove(keuze);
            _db.SaveChanges();
        }

        public IEnumerable<Keuze> GetAll()
        {
            return _db.Keuze.Include(a => a.Afstudeerrichting)
                            .Include(k => k.KeuzeOffer).ToList();
        }

        public IEnumerable<Keuze> GetAllByAfstudeerrichting(string id)
        {
            return _db.Keuze.Where(k => k.AfstudeerrichtingId == id).Include(a => a.Afstudeerrichting)
                .Include(k => k.KeuzeOffer).ToList();
        }

        public Keuze GetById(string id)
        {
            return _db.Keuze.Where(k => k.Id == id).Include(a => a.Afstudeerrichting)
                            .Include(k => k.KeuzeOffer).FirstOrDefault();
        }

        public void Update(Keuze keuze)
        {
            _db.Entry(keuze).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
