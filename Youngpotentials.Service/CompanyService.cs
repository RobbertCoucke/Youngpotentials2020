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
        Companies GetCompanyByUserId(int id);
        Companies CreateCompany(Companies company);
        void UpdateCompany(Companies company);
        void DeleteCompany(int id);
        IEnumerable<Companies> GetAllUnverified();
        IEnumerable<Companies> GetAllVerified();
        void Verify(int id);
    }
    public class CompanyService : ICompanyService
    {

        private ICompanyDAO _companyDAO;
        private IOfferDAO _offerDAO;

        public CompanyService(ICompanyDAO companyDAO, IOfferDAO offerDAO)
        {
            _companyDAO = companyDAO;
            _offerDAO = offerDAO;
        }
        public Companies CreateCompany(Companies company)
        {
            //set company as unverified
            company.Verified = false;
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

        public IEnumerable<Companies> GetAllVerified()
        {
            return _companyDAO.GetAllVerified();
        }

        public Companies GetCompanyById(int id)
        {
            return _companyDAO.GetCompanyById(id);
        }

        public Companies GetCompanyByUserId(int id)
        {
            return _companyDAO.GetCompanyByUserId(id);
        }

        public void UpdateCompany(Companies company)
        {
            try
            {
                _companyDAO.UpdateCompany(company);
                var offers = _offerDAO.GetAllOffersByCompany((int)company.Id);
                foreach (var offer in offers)
                {
                    offer.Verified = true;
                    _offerDAO.UpdateOffer(offer);
                }
            }
            catch(Exception e)
            {
                var exception = e.Message;
            }
        }

        public void Verify(int id)
        {

            var company = GetCompanyById(id);
            //company.Verified = true;
            //TODO verify every single offer of that company
        }
    }
}
