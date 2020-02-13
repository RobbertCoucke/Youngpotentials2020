using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.DAO
{
    public interface ICompanyDAO
    {
        IEnumerable<Companies> GetAllCompanies();
        Companies GetCompanyById(int id);
        Companies CreateCompany(Companies company);
        void UpdateCompany(Companies company);
        void DeleteCompany(int id);

    }
    public class CompanyDAO : ICompanyDAO
    {
        private YoungpotentialsContext _db;

        public CompanyDAO()
        {
            _db = new YoungpotentialsContext();
        }

        public Companies CreateCompany(Companies company)
        {
            _db.Entry(company).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _db.SaveChanges();
            return company;
        }

        public void DeleteCompany(int id)
        {
            var company = _db.Companies.FirstOrDefault(c => c.Id == id);
            _db.Companies.Remove(company);
            _db.SaveChanges();
        }

        public IEnumerable<Companies> GetAllCompanies()
        {
            return _db.Companies.Include(c => c.User).ToList();
        }

        public Companies GetCompanyById(int id)
        {
            return _db.Companies.Where(c => c.Id == id).Include(c => c.User).FirstOrDefault();
        }

        public void UpdateCompany(Companies company)
        {
            _db.Entry(company).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
