using GestionEstudiantesBackend.Models.DTOs;

namespace GestionEstudiantesBackend.Data.Interfaces
{
    public interface ICursoRepository
    {
        Task<ResponseRequest> ListaDeCursos();
    }
}
