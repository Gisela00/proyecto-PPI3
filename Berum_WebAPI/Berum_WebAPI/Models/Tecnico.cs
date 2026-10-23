using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Berum_WebAPI.Models;

[DynamoDBTable("Tecnicos")]
public partial class Tecnico
{
    [DynamoDBHashKey]
    public int IdTecnico { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Especialidad { get; set; }

    public string? Email { get; set; }


    [DynamoDBIgnore]
    public virtual ICollection<OrdenReparacion> OrdenReparacions { get; set; } = new List<OrdenReparacion>();

    [DynamoDBIgnore]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
