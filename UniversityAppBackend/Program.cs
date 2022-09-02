using Microsoft.EntityFrameworkCore;
using University.BusinessLogic.Courses;
using University.BusinessLogic.Students;
using University.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);

#region University Context
const string CONNECTION_NAME = "UniversityDB";
string connectionString = builder.Configuration.GetConnectionString(CONNECTION_NAME);
builder.Services.AddDbContext<UniversityDBContext>(options =>
{
    options.UseSqlServer(connectionString);
});
#endregion

//Add services for JWT Authorization


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICoursesService, CoursesService>();
builder.Services.AddTransient<IStudentsService, StudentsService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "PolicyCorsUniversityApi", corsBuilder =>
    {
        corsBuilder.AllowAnyOrigin();
        corsBuilder.AllowAnyMethod();
        corsBuilder.AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("PolicyCorsUniversityApi");

app.Run();
