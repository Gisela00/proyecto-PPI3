namespace Berum_WebAPI.DTOs
{
    public class DatosOrdenPdfDTO
    {
        public int IdOrden { get; set; }

        public string NumeroServicio { get; set; } = string.Empty;

        public DateTime FechaIngreso { get; set; }

        public string TipoEquipo { get; set; } = string.Empty;

        public string Marca { get; set; } = string.Empty;

        public string Modelo { get; set; } = string.Empty;

        public string DescripcionFalla { get; set; } = string.Empty;

        public string Cliente { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
