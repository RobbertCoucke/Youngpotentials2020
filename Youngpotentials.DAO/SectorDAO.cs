using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.DAO
{
    public interface ISectorDAO
    {
        IEnumerable<Sector> GetAll();
        Sector GetById(int id);
    }

    public class SectorDAO : ISectorDAO
    {
        private readonly YoungpotentialsV1Context _db;
        
        public SectorDAO()
        {
            _db = new YoungpotentialsV1Context();
        }

        public IEnumerable<Sector> GetAll()
        {
            try
            {
                return _db.Sector.ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public Sector GetById(int id)
        {
            try
            {
                return _db.Sector.Where(s => s.Id == id).FirstOrDefault();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
