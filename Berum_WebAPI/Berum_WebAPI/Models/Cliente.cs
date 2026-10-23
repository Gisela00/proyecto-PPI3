using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Berum_WebAPI.Models;

[DynamoDBTable("")] // Nombre de la tabla en DynamoDB
public partial class Cliente
{
    [DynamoDBHashKey]
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    [DynamoDBIgnore]
    public virtual ICollection<OrdenReparacion> OrdenReparacions { get; set; } = new List<OrdenReparacion>();
}