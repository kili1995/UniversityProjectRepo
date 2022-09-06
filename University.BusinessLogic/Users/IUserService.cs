namespace University.BusinessLogic.Users
{
    using University.BusinessLogic.Users.Dtos;
    using University.DataAccess.Models.DataModels;
    public interface IUserService
    {
        Task<List<User>> GetUsersByEmail(string email);

        Task<User?> GetUserByName(string name);

        Task<List<User>> GetAllUsers();

        Task<User> CreateUser(CreateUserDto user);

        Task<bool> ValidateUserByNameAndPassword(string userName, string password);
    }
}
