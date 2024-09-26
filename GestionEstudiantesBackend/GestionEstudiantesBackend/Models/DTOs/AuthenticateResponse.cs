using GestionEstudiantesBackend.Models.Entities;

namespace GestionEstudiantesBackend.Models.DTOs
{
    public class AuthenticateResponse
    {
        public long IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(UsuariosModel? user, string token)
        {
            IdUsuario = user.IdUsuario;
            Usuario = user.Usuario;
            Token = token;
        }
    }
}
