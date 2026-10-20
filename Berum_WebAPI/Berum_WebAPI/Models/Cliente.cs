using System;
using System.Collections.Generic;

namespace Berum_WebAPI.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<OrdenReparacion> OrdenReparacions { get; set; } = new List<OrdenReparacion>();
}
