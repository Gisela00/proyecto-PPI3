namespace Berum_WebAPI.DTOs
{
    public class HistorialEstadoDto
    {
        public int IdHistorial { get; set; }
        public int IdOrden { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string? Observaciones { get; set; }
    }
}
