using GestionEstudiantesBackend.Models.DTOs;

namespace GestionEstudiantesBackend.Data.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseRequest> Login(AuthenticateRequest user);
        Task<AuthenticateResponse> GetCurrentUser(string token);
    }
}
