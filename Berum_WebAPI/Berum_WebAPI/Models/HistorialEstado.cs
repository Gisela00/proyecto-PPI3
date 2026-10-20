using System;
using System.Collections.Generic;

namespace Berum_WebAPI.Models;

public partial class HistorialEstado
{
    public int IdHistorial { get; set; }

    public int IdOrden { get; set; }

    public int IdEstado { get; set; }

    public DateTime Fecha { get; set; }

    public string? Observaciones { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual OrdenReparacion IdOrdenNavigation { get; set; } = null!;
}
