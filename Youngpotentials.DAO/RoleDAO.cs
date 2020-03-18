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
        AspNetRoles GetRoleByName(string name);
    }
    public class RoleDAO : IRoleDAO
    {
        private YoungpotentialsV1Context _db;

        public RoleDAO()
        {
            _db = new YoungpotentialsV1Context();
        }

        public IEnumerable<AspNetRoles> GetAllRoles()
        {
            try
            {
                return _db.AspNetRoles.ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public AspNetRoles GetRoleById(int id)
        {
            try
            {
                return _db.AspNetRoles.Where(r => r.Id == id).FirstOrDefault();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public AspNetRoles GetRoleByName(string name)
        {
            try
            {
                return _db.AspNetRoles.Where(r => r.Name == name).FirstOrDefault();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
