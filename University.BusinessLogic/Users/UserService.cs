namespace University.BusinessLogic.Users
{
    using Microsoft.EntityFrameworkCore;
    using University.BusinessLogic.Users.Dtos;
    using University.DataAccess.Context;
    using University.DataAccess.Models.DataModels;
    public class UserService : IUserService
    {
        private readonly UniversityDBContext _context;

        public UserService(UniversityDBContext universityContext)
        {
            _context = universityContext;
        }

        public async Task<User> CreateUser(CreateUserDto user)
        {
            User newUser = null;
            if(_context.Users != null)
            {
                bool alreadyExistsUser = await _context.Users.AnyAsync(u => u.Username == user.Name);
                if (!alreadyExistsUser)
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    var newEntity = await _context.Users.AddAsync(new User()
                    {
                        CreatedBy = user.Name,
                        CreationDate = DateTime.Now,
                        Email = user.Email,
                        Username = user.Name,
                        Password = passwordHash                        
                    });
                    newUser = newEntity.Entity;
                    await _context.SaveChangesAsync();
                }
            }
            return newUser;
        }

        public async Task<List<User>> GetAllUsers()
        {
            List<User> users = new();
            if(_context.Users != null)
            {
                users = await _context.Users.ToListAsync();
            }
            return users;
        }

        public async Task<User?> GetUserByName(string name)
        {
            User? user = new();
            if(_context.Users != null)
            {
                user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username.Equals(name, StringComparison.OrdinalIgnoreCase));
            }
            return user;
        }

        public async Task<List<User>> GetUsersByEmail(string email)
        {
            List<User> users = new();
            if(_context.Users != null)
            {
                users = await _context.Users.Where(user => user.Email == email).ToListAsync();
            }
            return users;
        }

        public async Task<bool> ValidateUserByNameAndPassword(string userName, string password)
        {
            bool isValid = false;
            try
            {
                if(_context.Users != null)
                {
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Username.Equals(userName, StringComparison.OrdinalIgnoreCase));
                    if(user != null)
                    {
                        isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
                    }                    
                }
                
            }
            catch (Exception e)
            {

            }
            return isValid;
        }
    }
}
