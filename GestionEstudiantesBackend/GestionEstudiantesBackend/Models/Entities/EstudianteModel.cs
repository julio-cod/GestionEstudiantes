using System.ComponentModel.DataAnnotations;

namespace GestionEstudiantesBackend.Models.Entities
{
    public class EstudianteModel
    {
        [Key]
        public int IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdCurso { get; set; }
        public string Direccion { get; set; }
    }
}
