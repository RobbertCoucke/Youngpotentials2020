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
        IEnumerable<AspNetUsers> GetAdmins();
       
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
            try
            {
                _db.Entry(user).State = EntityState.Added;
                _db.SaveChanges(); 
                return user;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void DeleteUser(int id)
        {
            try
            {
                var record = _db.AspNetUsers.FirstOrDefault(x => x.Id == id);
                _db.AspNetUsers.Remove(record);
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AspNetUsers> GetAdmins()
        {
            try
            {
                return _db.AspNetUsers.Where(u => u.Role.Name == "Admin").ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public IEnumerable<AspNetUsers> GetAllUsers()
        {
            try
            {
                return _db.AspNetUsers.ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public AspNetUsers GetUserByEmail(string email)
        {
            try
            {
                return _db.AspNetUsers.Where(u => u.Email == email).Include(u => u.Role).FirstOrDefault();
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

        public AspNetUsers GetUserById(int id)
        {
            try
            {
                return _db.AspNetUsers.FirstOrDefault(u => u.Id == id);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void UpdateUser(AspNetUsers user)
        {
            try
            {
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}
