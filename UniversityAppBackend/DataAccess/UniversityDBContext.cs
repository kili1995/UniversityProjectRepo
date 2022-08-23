namespace UniversityAppBackend.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using UniversityAppBackend.Models.DataModels;

    public class UniversityDBContext : DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options) : base(options)
        {

        }

        public DbSet<User>? Users { get; set; }
    }
}
