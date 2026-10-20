using System;
using System.Collections.Generic;

namespace Berum_WebAPI.Models;

public partial class OrdenReparacion
{
    public int IdOrden { get; set; }

    public string NumeroServicio { get; set; } = null!;

    public DateTime FechaIngreso { get; set; }

    public int IdCliente { get; set; }

    public int IdEquipo { get; set; }

    public int IdTecnico { get; set; }

    public int IdEstadoActual { get; set; }

    public int? IdTipoFalla { get; set; }

    public string? DescripcionFalla { get; set; }

    public virtual ICollection<HistorialEstado> HistorialEstados { get; set; } = new List<HistorialEstado>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;

    public virtual Estado IdEstadoActualNavigation { get; set; } = null!;

    public virtual Tecnico IdTecnicoNavigation { get; set; } = null!;

    public virtual TipoFalla? IdTipoFallaNavigation { get; set; }
}
