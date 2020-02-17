using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.DAO;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.Service
{
    public interface IKeuzeService
    {
        IEnumerable<Keuze> GetAll();
        Keuze GetById(string id);
        IEnumerable<Keuze> GetAllByAfstudeerrichting(string id);
        void DeleteById(string id);
        void Update(Keuze keuze);
        Keuze CreateKeuze(Keuze keuze);
    }
    public class KeuzeService : IKeuzeService
    {
        private IKeuzeDAO _keuzeDAO;

        public KeuzeService(IKeuzeDAO keuzeDAO)
        {
            _keuzeDAO = keuzeDAO;
        }
        public Keuze CreateKeuze(Keuze keuze)
        {
            return _keuzeDAO.CreateKeuze(keuze);
        }

        public void DeleteById(string id)
        {
            _keuzeDAO.DeleteById(id);
        }

        public IEnumerable<Keuze> GetAll()
        {
            return _keuzeDAO.GetAll();
        }

        public IEnumerable<Keuze> GetAllByAfstudeerrichting(string id)
        {
            return _keuzeDAO.GetAllByAfstudeerrichting(id);
        }

        public Keuze GetById(string id)
        {
            return _keuzeDAO.GetById(id);
        }

        public void Update(Keuze keuze)
        {
            _keuzeDAO.Update(keuze);
        }
    }
}
