using University.DataAccess.Models.DataModels;

namespace University.BusinessLogic.Students
{
    public interface IStudentsService
    {
        Task<List<Student>> GetAdultStudents();

        Task<List<Student>> GetStudentsWithAlmostOneCourse();

        Task<List<Student>> GetStudentsWithoutCourse();

        Task<List<Course>> GetStudentCourse(int studentId);
    }
}
