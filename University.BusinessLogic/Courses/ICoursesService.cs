using University.DataAccess.Models.DataModels;

namespace University.BusinessLogic.Courses
{
    public interface ICoursesService
    {
        Task<List<Course>> GetCourseByLevelWithAlmostOneStudent(CourseLevel level);
        Task<List<Course>> GetCourseByLevelAndCategory(CourseLevel level, Category category);
        Task<List<Course>> GetCoursesWithoutStudents();
        Task<List<Course>> GetCoursesByCategory(string category);
        Task<List<Course>> GetCoursesWithoutCurriculum();
        Task<Course?> GetCourseById(int id);
        Task<List<Student>> GetStudentsFromCourse(int courseId);
    }
}
