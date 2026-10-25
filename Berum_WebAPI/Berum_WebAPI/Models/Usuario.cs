using Amazon.DynamoDBv2.DataModel;

namespace Berum_WebAPI.Models;

[DynamoDBTable("Usuarios")]
public partial class Usuario
{
    [DynamoDBHashKey]
    public int IdUsuario { get; set; }

    [DynamoDBGlobalSecondaryIndexHashKey("Usuario1-index")]
    public string Usuario1 { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public bool Activo { get; set; }

    public int? IdTecnico { get; set; }

    [DynamoDBIgnore]
    public virtual Tecnico? IdTecnicoNavigation { get; set; }
}