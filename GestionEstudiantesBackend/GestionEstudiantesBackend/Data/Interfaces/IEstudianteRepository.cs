using GestionEstudiantesBackend.Models.DTOs;

namespace GestionEstudiantesBackend.Data.Interfaces
{
    public interface IEstudianteRepository
    {
        Task<ResponseRequest> RegistrarEstudiante(RegistrarEstudianteDto registrarEstudianteDto);
        Task<ResponseRequest> EditarEstudiante(EditarEstudianteDto editarEstudianteDto);
        Task<ResponseRequest> EliminarEstudiante(EliminarEstudianteDto eliminarEstudianteDto);
        Task<ResponseRequest> ListaDeEstudiantes();
    }
}
