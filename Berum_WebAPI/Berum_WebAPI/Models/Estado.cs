using System;
using System.Collections.Generic;

namespace Berum_WebAPI.Models;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<HistorialEstado> HistorialEstados { get; set; } = new List<HistorialEstado>();

    public virtual ICollection<OrdenReparacion> OrdenReparacions { get; set; } = new List<OrdenReparacion>();
}
