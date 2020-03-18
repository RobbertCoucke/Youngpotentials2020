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
        private YoungpotentialsV1Context _db;

        public StudiegebiedDAO()
        {
            _db = new YoungpotentialsV1Context();
        }

        public Studiegebied CreateStudiegebied(Studiegebied studiegebied)
        {
            try
            {
                _db.Entry(studiegebied).State = EntityState.Added;
                _db.SaveChanges();
                return studiegebied;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteStudieGebied(string id)
        {
            try
            {
                var studiegebied = _db.Studiegebied.FirstOrDefault(s => s.Id == id);
                _db.Studiegebied.Remove(studiegebied);
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public IEnumerable<Studiegebied> GetAll()
        {
            try
            {
                return _db.Studiegebied.Include(o => o.Opleiding).ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Studiegebied GetById(string id)
        {
            try
            {
                return _db.Studiegebied.Where(s => s.Id == id)
                                       .Include(o => o.Opleiding)
                                       .Include(f => f.StudiegebiedOffer).FirstOrDefault();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void UpdateStudegebied(Studiegebied studiegebied)
        {
            try
            {
                _db.Entry(studiegebied).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
