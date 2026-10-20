namespace Berum_WebAPI.DTOs
{
    public class ComprobanteIngresoCreateDTO
    {
        public int IdOrden { get; set; }

        public List<string> AccesoriosEntregados { get; set; } = [];

        public List<string> EstadoInicial { get; set; } = [];
    }
}
