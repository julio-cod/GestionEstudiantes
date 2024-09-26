using GestionEstudiantesBackend.Data.DBContext;
using GestionEstudiantesBackend.Data.Interfaces;
using GestionEstudiantesBackend.Helpers;
using GestionEstudiantesBackend.Models.Entities;

namespace GestionEstudiantesBackend.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private GestionEstudianteDbContext _gestionEstudianteDbContext;
        public UsuarioRepository(GestionEstudianteDbContext gestionEstudianteDbContext)
        {
            _gestionEstudianteDbContext = gestionEstudianteDbContext;
        }

        public async Task<UsuariosModel?> GetUserById(long id)
        {
            try
            {
                var result = _gestionEstudianteDbContext.Usuarios.FirstOrDefault(x => x.IdUsuario == id);

                return result;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

    }
}
