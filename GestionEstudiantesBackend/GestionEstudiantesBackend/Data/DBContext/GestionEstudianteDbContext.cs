using System.Collections.Generic;
using GestionEstudiantesBackend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionEstudiantesBackend.Data.DBContext
{
    public class GestionEstudianteDbContext : DbContext
    {
        public GestionEstudianteDbContext(DbContextOptions<GestionEstudianteDbContext> options) : base(options)
        {
        }

        public DbSet<EstudianteModel> Estudiantes { get; set; }
        public DbSet<CursoModel> Cursos { get; set; }
        public DbSet<UsuariosModel> Usuarios { get; set; }
    }
}
