using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Youngpotentials.DAO;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.Service
{

    public interface IUserService
    {
        AspNetUsers Authenticate(string email, string password);
        IEnumerable<AspNetUsers> GetAll();
        AspNetUsers GetById(int id);
        AspNetUsers Create(AspNetUsers user, string password);
        void Update(AspNetUsers user, string password = null);
        void Delete(int id);
        AspNetUsers GetUserByEmail(string email);
        AspNetUsers ResetPassword(AspNetUsers user, string password);
        IEnumerable<AspNetUsers> GetAdmins();
    }

    public class UserService : IUserService
    {

        private IUserDAO _userDAO;
        private IStudentDAO _studentDAO;
        private ICompanyDAO _companyDAO;

        public UserService(IUserDAO userDAO, IStudentDAO studentDAO, ICompanyDAO companyDAO)
        {
            _userDAO = userDAO;
            _studentDAO = studentDAO;
            _companyDAO = companyDAO;
        }

        /// <summary>
        /// authenticates login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AspNetUsers Authenticate(string email, string password)
        {
            //check input
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            //check if user exists
            var user = _userDAO.GetUserByEmail(email);
            if (user == null)
                return null;

            //verify password
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public AspNetUsers ResetPassword(AspNetUsers user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("password is required");

            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userDAO.UpdateUser(user);
            return user;
        }

        public AspNetUsers Create(AspNetUsers user, string password)
        {
            //check input
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("password is required");
            }

            //check if email already exists
            if(_userDAO.GetUserByEmail(user.Email) != null)
            {
                throw new Exception("email is already taken");
            }

            //hash password
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;


            _userDAO.CreateUser(user);

            //TODO uncomment
            //string code = GetSHA1(user.Id.ToString()).Replace("/", "").Replace("+", "");
            //user.Code = code;
            //Update(user);
            return user;
        }

        public void Delete(int id)
        {
            _userDAO.DeleteUser(id);
        }

        public IEnumerable<AspNetUsers> GetAll()
        {
            return _userDAO.GetAllUsers();
        }

        public AspNetUsers GetById(int id)
        {
            return _userDAO.GetUserById(id);
        }

        public void Update(AspNetUsers user, string password = null)
        {
            _userDAO.UpdateUser(user);
        }


        /// <summary>
        /// hash a password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
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


        /// <summary>
        /// verify a password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
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

        public AspNetUsers GetUserByEmail(string email)
        {
            return _userDAO.GetUserByEmail(email);
        }

        private string GetSHA1(string input)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            using (SHA1 shaM = new SHA1Managed())
            {
                byte[] result = shaM.ComputeHash(data);
                return Convert.ToBase64String(result);
            }
        }

        public IEnumerable<AspNetUsers> GetAdmins()
        {
            return _userDAO.GetAdmins();
        }
    }
}
