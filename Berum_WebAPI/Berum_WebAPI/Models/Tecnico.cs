using System;
using System.Collections.Generic;

namespace Berum_WebAPI.Models;

public partial class Tecnico
{
    public int IdTecnico { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Especialidad { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<OrdenReparacion> OrdenReparacions { get; set; } = new List<OrdenReparacion>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
