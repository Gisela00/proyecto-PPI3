using Berum_WebAPI.DTOs;
using Berum_WebAPI.PDF.Presupuesto;

namespace Berum_WebAPI.PDF.ComprobanteIngreso
{
    public class ComprobanteIngresoModel
    {
        public int IdOrden { get; set; }
        public string NumeroServicio { get; set; } = string.Empty;
        public DateTime FechaIngreso { get; set; }
        public string TipoEquipo { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string ProblemaInformado { get; set; } = string.Empty;
        public string AvisoImportante { get; set; } =
            "La recepción del equipo no implica la aceptación automática de un presupuesto ni el inicio de la reparación.";
        public List<string> AccesoriosEntregados { get; set; } = [];
        public List<string> EstadoInicial { get; set; } = [];
        public List<string> ProximosPasos { get; set; } =
        [
            "Se realizará un diagnóstico técnico.",
            "Se generará un presupuesto para su aprobación.",
            "Una vez aprobado se procederá con la reparación.",
            "Podrá consultar el estado del servicio."
        ];
        public ClienteDTO Cliente { get; set; } = new();
    }
}
