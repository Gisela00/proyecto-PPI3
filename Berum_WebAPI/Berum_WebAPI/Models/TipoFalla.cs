using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Berum_WebAPI.Models;

[DynamoDBTable("TipoFallas")]
public partial class TipoFalla
{
    [DynamoDBHashKey]
    public int IdTipoFalla { get; set; }
    public string Nombre { get; set; } = null!;

    [DynamoDBIgnore]
    public virtual ICollection<OrdenReparacion> OrdenReparacions { get; set; } = new List<OrdenReparacion>();
}
