namespace Berum_WebAPI.DTOs
{
    public class TecnicoUpdateDTO
    {
        public string Nombre { get; set; } = string.Empty!;

        public string? Telefono { get; set; }

        public string? Especialidad { get; set; }

        public string? Email { get; set; }
    }
}
