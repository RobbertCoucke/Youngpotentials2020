using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.DAO
{

    public interface IRoleDAO
    {
        IEnumerable<AspNetRoles> GetAllRoles();
        AspNetRoles GetRoleById(int id);
    }
    public class RoleDAO : IRoleDAO
    {
        private YoungpotentialsContext _db;

        public RoleDAO()
        {
            _db = new YoungpotentialsContext();
        }

        public IEnumerable<AspNetRoles> GetAllRoles()
        {
            return _db.AspNetRoles.ToList();
        }

        public AspNetRoles GetRoleById(int id)
        {
            return _db.AspNetRoles.Where(r => r.Id).FirstOrDefault();
        }

    }
}
