namespace University.Api.Infraestructure
{
    using University.BusinessLogic.Courses;
    using University.BusinessLogic.Students;
    using University.BusinessLogic.Users;

    public static class DependencyInjection
    {
        public static void AddServicesToProject(
            this IServiceCollection services
        )
        {
            services.AddScoped<ICoursesService, CoursesService>();
            services.AddScoped<IStudentsService, StudentsService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
