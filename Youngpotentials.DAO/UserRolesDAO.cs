using System;
using System.Collections.Generic;
using System.Text;

namespace Youngpotentials.DAO
{
    public interface IUserRolesDAO
    {
        void AddUserRole(AspNetUserRoles userRole);
        void DeleteUserRole(AspNetUserRoles userRole);
    }
    class UserRolesDAO : IUserRolesDAO
    {

        private YoungpotentialsContext _db;

        public UserRolesDAO()
        {
            _db = new YoungpotentialsContext();
        }

        public void AddUserRole(AspNetUserRoles userRole)
        {
            _db.Entry(userRole).State = EntityStates.Added;
            _db.SaveChanges();
        }

        public void DeleteUserRole(AspNetUserRoles userRole)
        {
            _db.AspNetUserRoles.Remove(userRole);
            _db.SaveChanges();
        }



    }
}
