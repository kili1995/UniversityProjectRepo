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
            if(_context.Courses != null)
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
    }
}
