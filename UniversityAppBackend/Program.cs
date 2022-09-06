using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using University.Api.Helpers;
using University.Api.Infraestructure;
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
builder.Services.AddJwtTokenServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddServicesToProject();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Configuring Swagger Gen to work with authorization
builder.Services.AddSwaggerGen(options =>
{
    //Defining Security
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using BEARER Scheme."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

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
