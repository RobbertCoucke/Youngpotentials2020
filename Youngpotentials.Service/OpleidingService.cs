using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.DAO;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.Service
{
    public interface IOpleidingService
    {
        IEnumerable<Opleiding> GetAll();
        Opleiding GetById(string id);
        IEnumerable<Opleiding> GetAllByStudiegebied(string id);
        void DeleteById(string id);
        void Update(Opleiding opleiding);
        Opleiding CreateOpleiding(Opleiding opleiding);
    }
    public class OpleidingService : IOpleidingService
    {
        private IOpleidingDAO _opleidingDAO;

        public OpleidingService(IOpleidingDAO opleidingDAO)
        {
            _opleidingDAO = opleidingDAO;
        }
        public Opleiding CreateOpleiding(Opleiding opleiding)
        {
            return _opleidingDAO.CreateOpleiding(opleiding);
        }

        public void DeleteById(string id)
        {
            _opleidingDAO.DeleteById(id);
        }

        public IEnumerable<Opleiding> GetAll()
        {
            return _opleidingDAO.GetAll();
        }

        public IEnumerable<Opleiding> GetAllByStudiegebied(string id)
        {
            return _opleidingDAO.GetAllByStudiegebied(id);
        }

        public Opleiding GetById(string id)
        {
            return _opleidingDAO.GetById(id);
        }

        public void Update(Opleiding opleiding)
        {

            var o = GetById(opleiding.Id);
            string opleidingNaam = opleiding.NaamOpleiding;
            string studiegebiedId = opleiding.IdStudiegebied;

            if (opleidingNaam != "" && opleidingNaam != null)
            {
                o.NaamOpleiding = opleidingNaam;
            }

            if (studiegebiedId != null && studiegebiedId != "")
            {
                o.IdStudiegebied = studiegebiedId;
            }
            _opleidingDAO.Update(opleiding);

        }
    }
}
