using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.DAO;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.Service
{
    public interface IUserRoleService
    {
        IEnumerable<AspNetUserRoles> GetAll();
        AspNetUserRoles GetByUserId(int id);
        AspNetUserRoles CreateUserRole(AspNetUserRoles userRole);
        void DeleteUserRole(int id);
    }
    public class UserRoleService : IUserRoleService
    {
        private IUserRoleDAO _userRoleDAO;

        public UserRoleService(IUserRoleDAO userRoleDAO)
        {
            _userRoleDAO = userRoleDAO;
        }

        public AspNetUserRoles CreateUserRole(AspNetUserRoles userRole)
        {
            return _userRoleDAO.CreateUserRole(userRole);
        }

        public void DeleteUserRole(int id)
        {
            _userRoleDAO.DeleteUserRole(id);
        }

        public IEnumerable<AspNetUserRoles> GetAll()
        {
            return _userRoleDAO.GetAll();
        }

        public AspNetUserRoles GetByUserId(int id)
        {
            return _userRoleDAO.GetByUserId(id);
        }
    }
}
