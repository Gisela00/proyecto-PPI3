namespace Berum_WebAPI.DTOs
{
    public class EquipoCreateDTO
    {
        public string Modelo { get; set; } = null!;
        public int IdMarca { get; set; }
        public int IdTipoEquipo { get; set; }
    }
}
