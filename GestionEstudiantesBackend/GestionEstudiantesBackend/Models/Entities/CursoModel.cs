using System.ComponentModel.DataAnnotations;

namespace GestionEstudiantesBackend.Models.Entities
{
    public class CursoModel
    {
        [Key]
        public int IdCurso { get; set; }
        public string Descripcion { get; set; }
    }
}
