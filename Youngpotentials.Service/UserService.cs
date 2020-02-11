using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.Domain.testEntities;

namespace Youngpotentials.Service
{

    public interface IUserService
    {
        AspNetUser Authenticate(string email, string password);
        IEnumerable<AspNetUser> GetAll();
        AspNetUser GetById(int id);
        AspNetUser Create(AspNetUser user, string password);
        void Update(AspNetUser user, string password = null);
        void Delete(int id);
    }

    public class UserService : IUserService
    {

        private IUserDAO _userDAO;
        private IStudentDAO _studentDAO;
        private ICompanyDAO _companyDAO;
        private IDocentDAO _docentDAO;

        public UserService(IUserDAO userDAO, IStudentDAO studentDAO, ICompanyDAO companyDAO, IDocentDAO docentDAO)
        {
            _userDAO = userDAO;
            _studentDAO = studentDAO;
            _companyDAO = companyDAO;
            _docentDAO = docentDAO;
        }
        
        public AspNetUser Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _userDAO.GetUserByEmail(email);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public AspNetUser Create(AspNetUser user, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AspNetUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public AspNetUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(AspNetUser user, string password = null)
        {
            throw new NotImplementedException();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
