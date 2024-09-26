using System.ComponentModel.DataAnnotations;

namespace GestionEstudiantesBackend.Models.Entities
{
    public class UsuariosModel
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Tipo { get; set; }
    }
}
