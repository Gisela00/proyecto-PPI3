using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Berum_WebAPI.Models;

[DynamoDBTable("Marcas")]
public partial class Marca
{
    [DynamoDBHashKey]
    public int IdMarca { get; set; }

    public string Nombre { get; set; } = null!;

    [DynamoDBIgnore]
    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
}
