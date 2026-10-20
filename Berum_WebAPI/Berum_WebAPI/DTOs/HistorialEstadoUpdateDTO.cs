using System.ComponentModel.DataAnnotations;

namespace Berum_WebAPI.DTOs
{
    public class HistorialEstadoUpdateDTO
    {
        [MaxLength(500)]
        public string? Observaciones { get; set; }
    }
}
