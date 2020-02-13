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
        void UpdateStudents(Students student);
        void DeleteStudents(int id);
    }

    public class StudentDAO : IStudentDAO
    {
        private YoungpotentialsContext _db;

        public StudentDAO()
        {
            _db = new YoungpotentialsContext();
        }

        public Students CreateStudent(Students student)
        {
            _db.Entry(student).State = EntityState.Added;
            _db.SaveChanges();
            return student;
        }

        public void DeleteStudents(int id)
        {
            var student = _db.Students.FirstOrDefault(x => x.Id == id);
            _db.Students.Remove(student);
            _db.SaveChanges();
        }

        public IEnumerable<Students> GetAllStudents()
        {
            return _db.Students.Include(s => s.User).ToList();
        }

        public Students GetStudentById(int id)
        {
            return _db.Students.Where(s => s.Id == id).Include(s => s.User).FirstOrDefault();
        }

        public void UpdateStudents(Students student)
        {
            _db.Entry(student).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
