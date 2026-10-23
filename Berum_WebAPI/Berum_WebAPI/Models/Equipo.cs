using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Berum_WebAPI.Models;

[DynamoDBTable("Equipos")]

public partial class Equipo
{
    [DynamoDBHashKey]
    public int IdEquipo { get; set; }

    public int IdTipoEquipo { get; set; }

    public int IdMarca { get; set; }

    public string Modelo { get; set; } = null!;
    
    [DynamoDBIgnore]
    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    [DynamoDBIgnore]
    public virtual TipoEquipo IdTipoEquipoNavigation { get; set; } = null!;

    [DynamoDBIgnore]
    public virtual ICollection<OrdenReparacion> OrdenReparacions { get; set; } = new List<OrdenReparacion>();
}
