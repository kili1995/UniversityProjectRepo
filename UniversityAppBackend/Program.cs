using Microsoft.EntityFrameworkCore;
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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
