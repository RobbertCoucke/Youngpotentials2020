using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.DAO;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.Service
{
    public interface IAfstudeerrichtingService
    {
        IEnumerable<Afstudeerrichting> GetAll();
        Afstudeerrichting GetById(string id);
        IEnumerable<Afstudeerrichting> GetAllByOpleiding(string id);
        void DeleteById(string id);
        void Update(Afstudeerrichting afstudeerrichting);
        Afstudeerrichting CreateAfstudeerrichting(Afstudeerrichting afstudeerrichting);
    }
    public class AfstudeerrichtingService : IAfstudeerrichtingService
    {
        private IAfstudeerrichtingDAO _afstudeerrichtingDAO;

        public AfstudeerrichtingService(IAfstudeerrichtingDAO afstudeerrichting)
        {
            _afstudeerrichtingDAO = afstudeerrichting;
        }
        public Afstudeerrichting CreateAfstudeerrichting(Afstudeerrichting afstudeerrichting)
        {
            return _afstudeerrichtingDAO.CreateAfstudeerrichting(afstudeerrichting);
        }

        public void DeleteById(string id)
        {
            _afstudeerrichtingDAO.DeleteById(id);
        }

        public IEnumerable<Afstudeerrichting> GetAll()
        {
            return _afstudeerrichtingDAO.GetAll();
        }

        public IEnumerable<Afstudeerrichting> GetAllByOpleiding(string id)
        {
            return _afstudeerrichtingDAO.GetAllByOpleiding(id);
        }

        public Afstudeerrichting GetById(string id)
        {
            return _afstudeerrichtingDAO.GetById(id);
        }

        public void Update(Afstudeerrichting afstudeerrichting)
        {
            var a = GetById(afstudeerrichting.Id);
            string afstudeerrichtingNaam = afstudeerrichting.AfstudeerrichtingNaam.Trim();
            string opleidingId = afstudeerrichting.OpleidingId.Trim();
            if (afstudeerrichtingNaam != "" && afstudeerrichtingNaam != null)
            {
                a.AfstudeerrichtingNaam = afstudeerrichtingNaam;
            }

            if (opleidingId != null && opleidingId != "")
            {
                a.OpleidingId = opleidingId;
            }
            _afstudeerrichtingDAO.Update(afstudeerrichting);
        }
    }
}
