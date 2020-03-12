﻿using Microsoft.EntityFrameworkCore;
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
        Companies GetCompanyByUserId(int id);
        Companies CreateCompany(Companies company);
        void UpdateCompany(Companies company);
        void DeleteCompany(int id);
        IEnumerable<Companies> GetAllUnverified();
        IEnumerable<Companies> GetAllVerified();

    }
    public class CompanyDAO : ICompanyDAO
    {
        private YoungpotentialsV1Context _db;

        public CompanyDAO()
        {
            _db = new YoungpotentialsV1Context();
        }


        //gets all verified companies
        public IEnumerable<Companies> GetAllVerified()
        {
            return _db.Companies.Where(c => c.Verified != false).Include(c => c.Offers).Include(c => c.User).Include(c => c.Sector).ToList();
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

        //gets all unverified companies
        public IEnumerable<Companies> GetAllUnverified()
        {
            return _db.Companies.Where(c => c.Verified == false).Include(c => c.Offers).Include(c => c.User).Include(c => c.Sector).ToList();
        }

        //gets company by companyId (primary key)
        public Companies GetCompanyById(int id)
        {
            return _db.Companies.Where(c => c.Id == id).Include(c => c.User).Include(c => c.Sector).FirstOrDefault();
        }


        //gets company by userId (foreign key)
        public Companies GetCompanyByUserId(int id)
        {
            return _db.Companies.Where(c => c.UserId == id).Include(c => c.User).Include(c => c.Sector).FirstOrDefault();
        }

        public void UpdateCompany(Companies company)
        {
            _db.Entry(company).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges(); 
        }
    }
}
