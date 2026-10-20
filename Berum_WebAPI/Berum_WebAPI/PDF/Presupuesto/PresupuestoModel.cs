
using Berum_WebAPI.DTOs;
namespace Berum_WebAPI.PDF.Presupuesto
{
    public enum EstadoPresupuesto
    {
        Pendiente,
        Aprobado,
        Rechazado,
        Vencido
    }
    public class PresupuestoModel
    {
        public int IdPresupuesto { get; set; }
        public int IdOrden { get; set; }
        public string NumeroServicio { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento => FechaEmision.AddDays(10);
        public decimal Monto { get; set; }
        public string TipoEquipo { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public List<string> DescripcionTrabajo { get; set; } = [];
        public List<string> FallasDetectadas { get; set; } = [];
        public List<string> ObservacionesTecnico { get; set; } = [];
        public EstadoPresupuesto EstadoPresupuesto { get; set; }
        public ClienteDTO Cliente { get; set; } = new();
    }
}
