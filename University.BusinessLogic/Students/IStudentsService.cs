using University.DataAccess.Models.DataModels;

namespace University.BusinessLogic.Students
{
    public interface IStudentsService
    {
        Task<List<Student>> GetAdultStudents();

        Task<List<Student>> GetStudentsWithAlmostOneCourse();
    }
}
