using System.ComponentModel.DataAnnotations;
namespace Berum_WebAPI.DTOs
{
    public class HistorialEstadoCreateDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int IdOrden { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int IdEstado { get; set; }

        [MaxLength(500)]
        public string? Observaciones { get; set; }
    }
}
