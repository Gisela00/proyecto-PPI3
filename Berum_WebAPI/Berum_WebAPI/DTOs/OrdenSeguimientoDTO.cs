namespace Berum_WebAPI.DTOs
{
    public class OrdenSeguimientoDTO
    {
        public string NumeroServicio { get; set; } = null!;
        public string DescripcionFalla { get; set; } = null!;
        public string TipoEquipo { get; set; } = null!;
        public string MarcaEquipo { get; set; } = null!;
        public string ModeloEquipo { get; set; } = null!;
        public string TecnicoAsignado { get; set; } = null!;
        public List<HistorialEstadoDto> HistorialEstados { get; set; } = new();
    }
}
