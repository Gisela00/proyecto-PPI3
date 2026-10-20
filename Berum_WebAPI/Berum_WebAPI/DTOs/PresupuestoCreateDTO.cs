namespace Berum_WebAPI.DTOs
{
    public class PresupuestoCreateDTO
    {
        public int IdOrden { get; set; }
        public decimal Monto { get; set; }
        public List<string> DescripcionTrabajo { get; set; } = [];
        public List<string> ObservacionesTecnico { get; set; } = [];

    }
}
