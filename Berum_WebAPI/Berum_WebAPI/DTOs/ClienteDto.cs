namespace Berum_WebAPI.DTOs
{
    public class ClienteDTO
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Email { get; set; }
        public string? Telefono { get; set; }
    }
}
