using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.DAO;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.Service
{
    public interface IStudiegebiedService
    {
        IEnumerable<Studiegebied> GetAll();
        Studiegebied GetById(string id);
        Studiegebied CreateStudiegebied(Studiegebied studiegebied);
        void UpdateStudegebied(Studiegebied studiegebied);
        void DeleteStudieGebied(string id);
    }
    public class StudiegebiedService : IStudiegebiedService
    {
        private IStudiegebiedDAO _studiegebiedDAO;

        public StudiegebiedService(IStudiegebiedDAO studiegebiedDAO)
        {
            _studiegebiedDAO = studiegebiedDAO;
        }
        public Studiegebied CreateStudiegebied(Studiegebied studiegebied)
        {
            return _studiegebiedDAO.CreateStudiegebied(studiegebied);
        }

        public void DeleteStudieGebied(string id)
        {
            _studiegebiedDAO.DeleteStudieGebied(id);
        }

        public IEnumerable<Studiegebied> GetAll()
        {
            return _studiegebiedDAO.GetAll();
        }

        public Studiegebied GetById(string id)
        {
            return _studiegebiedDAO.GetById(id);
        }

        public void UpdateStudegebied(Studiegebied studiegebied)
        {
            var s = GetById(studiegebied.Id);
            string studiegebiedNaam = studiegebied.Studiegebied1.Trim();
            string kleur = studiegebied.Kleur.Trim();
            if(studiegebiedNaam != "" && studiegebiedNaam != null)
            {
                s.Studiegebied1 = studiegebiedNaam;
            }

            if (kleur != null && kleur != "")
            {
                s.Kleur = kleur;
            }
            _studiegebiedDAO.UpdateStudegebied(s);
        }
    }
}
