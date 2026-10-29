using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Berum_WebAPI.Models;

[DynamoDBTable("OrdenesReparacion")]
public partial class OrdenReparacion
{
    [DynamoDBHashKey]
    public int IdOrden { get; set; }

    [DynamoDBGlobalSecondaryIndexHashKey("NumeroServicio-index")]
    public string NumeroServicio { get; set; } = null!;

    public DateTime FechaIngreso { get; set; }

    public int IdCliente { get; set; }

    public int IdEquipo { get; set; }

    public int? IdTecnico { get; set; }

    public int IdEstadoActual { get; set; }

    public int? IdTipoFalla { get; set; }

    public string? DescripcionFalla { get; set; }
    
    [DynamoDBIgnore]
    public virtual ICollection<HistorialEstado> HistorialEstados { get; set; } = new List<HistorialEstado>();

    [DynamoDBIgnore]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    [DynamoDBIgnore]
    public virtual Equipo IdEquipoNavigation { get; set; } = null!;

    [DynamoDBIgnore]
    public virtual Estado IdEstadoActualNavigation { get; set; } = null!;
    
    [DynamoDBIgnore]
    public virtual Tecnico IdTecnicoNavigation { get; set; } = null!;

    [DynamoDBIgnore]
    public virtual TipoFalla? IdTipoFallaNavigation { get; set; }
}