using Microsoft.EntityFrameworkCore;
using University.DataAccess.Context;
using University.DataAccess.Models.DataModels;

namespace University.BusinessLogic.Courses
{
    public class CoursesService : ICoursesService
    {
        private readonly UniversityDBContext _context;

        public CoursesService(UniversityDBContext context)
        {
            _context = context;
        }
        public async Task<List<Course>> GetCourseByLevelAndCategory(CourseLevel level, Category category)
        {
            List<Course> courses = new List<Course>();
            if (_context.Courses != null)
            {
                courses = await _context.Courses.Where(course => course.CourseLevel == level &&
                    course.CourseCategories.Contains(category)).ToListAsync();
            }
            return courses;
        }

        public async Task<List<Course>> GetCourseByLevelWithAlmostOneStudent(CourseLevel level)
        {
            List<Course> courses = new List<Course>();
            if (_context.Courses != null)
            {
                courses = await _context.Courses.Where(course => course.CourseLevel == level).ToListAsync();
            }
            return courses;
        }

        public async Task<List<Course>> GetCoursesWithoutStudents()
        {
            List<Course> courses = new List<Course>();
            if (_context.Courses != null)
            {
                courses = await _context.Courses.Where(course => !course.Students.Any()).ToListAsync();
            }
            return courses;
        }

        public async Task<List<Course>> GetCoursesByCategory(string category)
        {
            List<Course> courses = new List<Course>();
            if (_context.Courses != null)
            {
                courses = await _context.Courses
                    .Where(course => !course.CourseCategories.Any(x => x.Name == category))
                    .ToListAsync();
            }
            return courses;
        }

        public async Task<List<Course>> GetCoursesWithoutCurriculum()
        {
            List<Course> courses = new List<Course>();
            if (_context.Courses != null)
            {
                courses = await _context.Courses
                    .Where(course => course.Curriculum == null)
                    .ToListAsync();
            }
            return courses;
        }

        public async Task<Course?> GetCourseById(int id)
        {
            Course? course = new();
            if (_context.Courses != null)
            {
                course = await _context.Courses
                    .Where(course => course.Id == id)
                    .FirstOrDefaultAsync();                
            }
            return course;
        }

        public async Task<List<Student>> GetStudentsFromCourse(int courseId)
        {
            List<Student> students = new List<Student>();
            if (_context.Courses != null)
            {
                students = await _context.Courses
                    .Where(course => course.Id == courseId)
                    .SelectMany(x => x.Students)
                    .ToListAsync();
            }
            return students;
        }
    }
}
