using Microsoft.EntityFrameworkCore;
using University.DataAccess.Context;
using University.DataAccess.Models.DataModels;

namespace University.BusinessLogic.Students
{
    public class StudentsService : IStudentsService
    {
        private readonly UniversityDBContext _context;

        public StudentsService(UniversityDBContext context)
        {
            _context = context;
        }
        public async Task<List<Student>> GetAdultStudents()
        {
            List<Student> students = new ();
            if(_context.Students != null)
            {
                students = await _context.Students.Where(student => student.Age >= 18).ToListAsync();
            }
            return students;
        }

        public async Task<List<Course>> GetStudentCourse(int studentId)
        {
            List<Course> courses = new ();
            if(_context.Students != null)
            {
                var student = await _context.Students
                                .FirstOrDefaultAsync(student => student.Id == studentId);
                if (student != null)
                {
                    courses = student.Courses.ToList();
                }
            }
            return courses;
        }

        public async Task<List<Student>> GetStudentsWithAlmostOneCourse()
        {
            List<Student> students = new();
            if(_context.Students != null)
            {
                students = await _context.Students.Where(student => student.Courses.Any()).ToListAsync();
            }
            return students;
        }

        public async Task<List<Student>> GetStudentsWithoutCourse()
        {
            List<Student> students = new();
            if (_context.Students != null)
            {
                students = await _context.Students.Where(student => !student.Courses.Any())
                    .ToListAsync();
            }
            return students;
        }
    }
}
