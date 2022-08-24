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
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<Curriculum>? Curricula { get; set; }
    }
}
