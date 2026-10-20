namespace Berum_WebAPI.DTOs
{
    public class EquipoUpdateDTO
    {
        public int Id { get; set; }

        public string? Modelo { get; set; }

        public int IdMarca { get; set; }

        public int IdTipoEquipo { get; set; }
    }
}
