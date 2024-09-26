using GestionEstudiantesBackend.Models.Entities;

namespace GestionEstudiantesBackend.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<UsuariosModel?> GetUserById(long id);
    }
}
