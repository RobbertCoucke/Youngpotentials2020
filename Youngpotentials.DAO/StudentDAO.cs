using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.DAO
{
    public interface IStudentDAO
    {
        IEnumerable<Students> GetAllStudents();
        Students GetStudentById(int id);
        Students CreateStudent(Students student);
        void UpdateStudent(Students student);
        void DeleteStudent(int id);
        Students GetStudentByUserId(int id);
    }

    public class StudentDAO : IStudentDAO
    {
        private YoungpotentialsV1Context _db;

        public StudentDAO()
        {
            _db = new YoungpotentialsV1Context();
        }

        public Students CreateStudent(Students student)
        {
            try
            {
               _db.Entry(student).State = EntityState.Added;
                _db.SaveChanges();
                return student;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
 
        }



        public void DeleteStudent(int id)
        {
            try
            {
                var student = _db.Students.FirstOrDefault(x => x.Id == id);
                _db.Students.Remove(student);
                _db.SaveChanges();
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public IEnumerable<Students> GetAllStudents()
        {
            try
            {
                return _db.Students.Include(s => s.User).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public Students GetStudentById(int id)
        {
            try
            {
                return _db.Students.Where(s => s.Id == id).Include(s => s.User).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public Students GetStudentByUserId(int id)
        {
            try
            {
                return _db.Students.Where(s => s.UserId == id).Include(s => s.User).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void UpdateStudent(Students student)
        {
            try
            {
                _db.Entry(student).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
