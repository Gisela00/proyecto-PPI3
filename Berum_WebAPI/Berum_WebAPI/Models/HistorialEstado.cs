using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Berum_WebAPI.Models;

[DynamoDBTable("HistorialEstados")]

public partial class HistorialEstado
{
    [DynamoDBHashKey]
    public int IdHistorial { get; set; }

    [DynamoDBGlobalSecondaryIndexHashKey("IdOrden-Fecha-index")]
    public int IdOrden { get; set; }

    public int IdEstado { get; set; }

    [DynamoDBGlobalSecondaryIndexRangeKey("IdOrden-Fecha-index")]
    public DateTime Fecha { get; set; }

    public string? Observaciones { get; set; }

    [DynamoDBIgnore]
    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    [DynamoDBIgnore]
    public virtual OrdenReparacion IdOrdenNavigation { get; set; } = null!;
}
