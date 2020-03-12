using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.DAO;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.Service
{
    public interface ISectorService
    {
        IEnumerable<Sector> GetAll();
        Sector GetById(int id);
    }
    public class SectorService : ISectorService
    {
        private ISectorDAO _sectorDAO;

        public SectorService(ISectorDAO sectorDAO)
        {
            _sectorDAO = sectorDAO;
        }
        public IEnumerable<Sector> GetAll()
        {
            return _sectorDAO.GetAll();
        }

        public Sector GetById(int id)
        {
            return _sectorDAO.GetById(id);
        }
    }
}
