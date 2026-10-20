using System;
using System.Collections.Generic;

namespace Berum_WebAPI.Models;

public partial class Equipo
{
    public int IdEquipo { get; set; }

    public int IdTipoEquipo { get; set; }

    public int IdMarca { get; set; }

    public string Modelo { get; set; } = null!;

    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    public virtual TipoEquipo IdTipoEquipoNavigation { get; set; } = null!;

    public virtual ICollection<OrdenReparacion> OrdenReparacions { get; set; } = new List<OrdenReparacion>();
}
