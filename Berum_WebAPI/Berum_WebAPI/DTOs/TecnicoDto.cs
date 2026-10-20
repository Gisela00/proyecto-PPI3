namespace Berum_WebAPI.DTOs
{
    public class TecnicoDto
    {
        public int IdTecnico { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Especialidad { get; set; }
        public string? Email { get; set; }
    }
}
