namespace Berum_WebAPI.DTOs
{
    public class OrdenReparacionDTO {
    
        public int IdOrden { get; set; }
        public string? NumeroServicio { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int IdCliente { get; set; }
        public int IdEquipo {get; set; }
        public int IdTecnico {get; set; }
        public int IdEstadoActual {get; set; }
        public int? IdTipoFalla {get; set; }
        public string DescripcionFalla {get; set; }
    
    }
}
