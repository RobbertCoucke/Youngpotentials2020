using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.DAO
{
    public interface IUserRoleDAO
    {
        IEnumerable<AspNetUserRoles> GetAll();
        AspNetUserRoles GetByUserId(int id);
        AspNetUserRoles CreateUserRole(AspNetUserRoles userRole);
        void DeleteUserRole(int id);
    }
    public class UserRoleDAO : IUserRoleDAO
    {
        private YoungpotentialsContext _db;

        public UserRoleDAO()
        {
            _db = new YoungpotentialsContext();
        }

        public AspNetUserRoles CreateUserRole(AspNetUserRoles userRole)
        {
            _db.Entry(userRole).State = EntityState.Added;
            _db.SaveChanges();
            return userRole;
        }

        public void DeleteUserRole(int id)
        {
            var record = _db.AspNetUserRoles.FirstOrDefault(x => x.UserId == id);
            _db.AspNetUserRoles.Remove(record);
            _db.SaveChanges();
        }

        public IEnumerable<AspNetUserRoles> GetAll()
        {
            return _db.AspNetUserRoles.ToList();
        }

        public AspNetUserRoles GetByUserId(int id)
        {
            return _db.AspNetUserRoles.Where(r => r.UserId == id).FirstOrDefault();
        }
    }
}
