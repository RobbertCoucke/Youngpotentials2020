using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.DAO;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.Service
{
    public interface IStudentService
    {
        IEnumerable<Students> GetAllStudents();
        Students GetStudentById(int id);
        Students CreateStudent(Students student);
        void UpdateStudent(Students student);
        void DeleteStudent(int id);
    }
    public class StudentService : IStudentService
    {

        private IStudentDAO _studentDAO;

        public StudentService(IStudentDAO studentDAO)
        {
            _studentDAO = studentDAO;
        }
        public Students CreateStudent(Students student)
        {

            return _studentDAO.CreateStudent(student);
        }

        public void DeleteStudent(int id)
        {
            _studentDAO.DeleteStudent(id);
        }

        public IEnumerable<Students> GetAllStudents()
        {
            return _studentDAO.GetAllStudents();
        }

        public Students GetStudentById(int id)
        {
            return _studentDAO.GetStudentById(id);
        }

        public void UpdateStudent(Students student)
        {
            _studentDAO.UpdateStudent(student);
        }
    }
}
