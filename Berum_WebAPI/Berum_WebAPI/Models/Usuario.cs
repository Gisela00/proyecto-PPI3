namespace Berum_WebAPI.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }

        public string Usuario1 { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Rol { get; set; } = null!;

        public bool Activo { get; set; }

        public int? IdTecnico { get; set; }

        public virtual Tecnico? IdTecnicoNavigation { get; set; }
    }
}
