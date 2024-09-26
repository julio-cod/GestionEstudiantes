using GestionEstudiantesBackend.Data.DBContext;
using GestionEstudiantesBackend.Data.Interfaces;
using GestionEstudiantesBackend.Data.Repositories;
using GestionEstudiantesBackend.Helpers;
using GestionEstudiantesBackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// SQL Server section
builder.Services.AddDbContext<GestionEstudianteDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnString_GestionEstudiantes")));

//Dependency Injection
builder.Services.AddTransient<IEstudianteRepository, EstudianteRepository>();
builder.Services.AddTransient<ICursoRepository, CursoRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IAuthService, AuthService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options =>
{
    //options.WithOrigins("http://localhost:3000");
    //options.WithOrigins("http://l72.30.3.60:81");
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();

});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
