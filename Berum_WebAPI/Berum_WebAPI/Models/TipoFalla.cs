using System;
using System.Collections.Generic;

namespace Berum_WebAPI.Models;

public partial class TipoFalla
{
    public int IdTipoFalla { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<OrdenReparacion> OrdenReparacions { get; set; } = new List<OrdenReparacion>();
}
