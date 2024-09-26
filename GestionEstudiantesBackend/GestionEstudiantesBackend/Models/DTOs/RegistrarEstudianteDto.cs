namespace GestionEstudiantesBackend.Models.DTOs
{
    public class RegistrarEstudianteDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdCurso { get; set; }
        public string Direccion { get; set; }
    }
}
