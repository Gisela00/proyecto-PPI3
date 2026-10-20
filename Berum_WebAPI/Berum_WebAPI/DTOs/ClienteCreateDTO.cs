namespace Berum_WebAPI.DTOs
{
    public class ClienteCreateDto
    {
        public string Nombre { get; set; } = null!;
        public string? Email { get; set; }
        public string? Telefono { get; set; }
    }
}
