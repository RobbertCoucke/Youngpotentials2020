using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.DAO;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.Service
{
    public interface IRoleService
    {
        IEnumerable<AspNetRoles> GetAllRoles();
        AspNetRoles GetRoleById(int id);
    }
    public class RoleService : IRoleService
    {

        private IRoleDAO _roleDAO;

        public RoleService(IRoleDAO roleDAO)
        {
            _roleDAO = roleDAO;
        }

        public IEnumerable<AspNetRoles> GetAllRoles()
        {
            return _roleDAO.GetAllRoles();
        }

        public AspNetRoles GetRoleById(int id)
        {
            return _roleDAO.GetRoleById(id);
        }
    }
}
