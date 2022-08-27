﻿namespace University.BusinessLogic.Users
{
    using Microsoft.EntityFrameworkCore;
    using University.DataAccess.Context;
    using University.DataAccess.Models.DataModels;
    public class UserService : IUserService
    {
        private readonly UniversityDBContext _context;

        public UserService(UniversityDBContext universityContext)
        {
            _context = universityContext;
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
    }
}