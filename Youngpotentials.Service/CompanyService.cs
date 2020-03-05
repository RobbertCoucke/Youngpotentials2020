using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.DAO;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.Service
{
    public interface ICompanyService
    {
        IEnumerable<Companies> GetAllCompanies();
        Companies GetCompanyById(int id);
        Companies CreateCompany(Companies company);
        void UpdateCompany(Companies company);
        void DeleteCompany(int id);
        IEnumerable<Companies> GetAllUnverified();
        void Verify(int id);
    }
    public class CompanyService : ICompanyService
    {

        private ICompanyDAO _companyDAO;

        public CompanyService(ICompanyDAO companyDAO)
        {
            _companyDAO = companyDAO;
        }
        public Companies CreateCompany(Companies company)
        {
            return _companyDAO.CreateCompany(company);
        }

        public void DeleteCompany(int id)
        {
            _companyDAO.DeleteCompany(id);
        }

        public IEnumerable<Companies> GetAllCompanies()
        {
            return _companyDAO.GetAllCompanies();
        }

        public IEnumerable<Companies> GetAllUnverified()
        {
            return _companyDAO.GetAllUnverified();
        }

        public Companies GetCompanyById(int id)
        {
            return _companyDAO.GetCompanyById(id);
        }

  

        public void UpdateCompany(Companies company)
        {
            _companyDAO.UpdateCompany(company);
        }

        public void Verify(int id)
        {

            var company = GetCompanyById(id);
            //company.Verified = true;
            //TODO verify every single offer of that company
        }
    }
}
