using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youngpotentials.Domain;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.DAO
{

    public interface IUserDAO
    {
        AspNetUsers GetUserByEmail(string email);
        AspNetUsers CreateUser(AspNetUsers user);
        void UpdateUser(AspNetUsers user);
        IEnumerable<AspNetUsers> GetAllUsers();
        void DeleteUser(int id);
        AspNetUsers GetUserById(int id);
       
    }
    public class UserDAO : IUserDAO
    {

        private YoungpotentialsV1Context _db;

        public UserDAO()
        {
            _db = new YoungpotentialsV1Context();
        }

        public AspNetUsers CreateUser(AspNetUsers user)
        {
            _db.Entry(user).State = EntityState.Added;
            _db.SaveChanges();
            return user;
        }

        public void DeleteUser(int id)
        {
            var record = _db.AspNetUsers.FirstOrDefault(x => x.Id == id);
            _db.AspNetUsers.Remove(record);
            _db.SaveChanges();
        }

        public IEnumerable<AspNetUsers> GetAllUsers()
        {
            return _db.AspNetUsers.ToList();
        }

        public AspNetUsers GetUserByEmail(string email)
        {
           return _db.AspNetUsers.Where(u => u.Email == email).Include(u=> u.Role).FirstOrDefault();
        }

        public AspNetUsers GetUserById(int id)
        {
            return _db.AspNetUsers.FirstOrDefault(u => u.Id == id);
        }

        public void UpdateUser(AspNetUsers user)
        {
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();
            
        }
    }
}
