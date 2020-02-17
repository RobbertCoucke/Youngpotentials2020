using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.DAO
{
    public interface IStudiegebiedDAO
    {
        IEnumerable<Studiegebied> GetAll();
        Studiegebied GetById(string id);
        Studiegebied CreateStudiegebied(Studiegebied studiegebied);
        void UpdateStudegebied(Studiegebied studiegebied);
        void DeleteStudieGebied(string id);
    }
    public class StudiegebiedDAO : IStudiegebiedDAO
    {
        private YoungpotentialsContext _db;

        public StudiegebiedDAO()
        {
            _db = new YoungpotentialsContext();
        }

        public Studiegebied CreateStudiegebied(Studiegebied studiegebied)
        {
            _db.Entry(studiegebied).State = EntityState.Added;
            _db.SaveChanges();
            return studiegebied;
        }

        public void DeleteStudieGebied(string id)
        {
            var studiegebied = _db.Studiegebied.FirstOrDefault(s => s.Id == id);
            _db.Studiegebied.Remove(studiegebied);
            _db.SaveChanges();
        }

        public IEnumerable<Studiegebied> GetAll()
        {
            return _db.Studiegebied.Include(o => o.Opleiding)
                                   .Include(f => f.StudiegebiedOffer).ToList();
        }

        public Studiegebied GetById(string id)
        {
            return _db.Studiegebied.Where(s => s.Id == id)
                                   .Include(o => o.Opleiding)
                                   .Include(f => f.StudiegebiedOffer).FirstOrDefault();
        }

        public void UpdateStudegebied(Studiegebied studiegebied)
        {
            _db.Entry(studiegebied).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
