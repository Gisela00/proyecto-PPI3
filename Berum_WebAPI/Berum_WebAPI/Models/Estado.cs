using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Berum_WebAPI.Models;

[DynamoDBTable("Estados")]

public partial class Estado
{
    [DynamoDBHashKey]
    public int IdEstado { get; set; }
    public string Nombre { get; set; } = null!;


    [DynamoDBIgnore]
    public virtual ICollection<HistorialEstado> HistorialEstados { get; set; } = new List<HistorialEstado>();

    [DynamoDBIgnore]
    public virtual ICollection<OrdenReparacion> OrdenReparacions { get; set; } = new List<OrdenReparacion>();
}
