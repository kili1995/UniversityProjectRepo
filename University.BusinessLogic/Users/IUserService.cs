using University.DataAccess.Models.DataModels;

namespace University.BusinessLogic.Users
{
    public interface IUserService
    {
        Task<List<User>> GetUsersByEmail(string email);

        Task<User?> GetUserByName(string name);

        Task<List<User>> GetAllUsers();
    }
}
