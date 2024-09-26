using System.ComponentModel.DataAnnotations;

namespace GestionEstudiantesBackend.Models.DTOs
{
    public class AuthenticateRequest
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Clave { get; set; }
    }
}
