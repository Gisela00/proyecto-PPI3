using System;
using System.Collections.Generic;

namespace Berum_WebAPI.Models;

public partial class TipoEquipo
{
    public int IdTipoEquipo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
}
