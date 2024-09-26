namespace GestionEstudiantesBackend.Models.DTOs
{
    public class ResponseRequest
    {
        public bool Operation { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
