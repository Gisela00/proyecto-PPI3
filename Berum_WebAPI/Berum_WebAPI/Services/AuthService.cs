using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;

namespace Berum_WebAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDynamoDBContext _dynamoContext;
        private readonly ITokenService _tokenService;

        public AuthService(IDynamoDBContext dynamoContext, ITokenService tokenService)
        {
            _dynamoContext = dynamoContext;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
        
            var porUsuario = await _dynamoContext
                .QueryAsync<Usuario>(request.Usuario, new QueryConfig { IndexName = "Usuario1-index" })
                .GetRemainingAsync();

            Usuario? usuario = porUsuario.FirstOrDefault(u => u.Activo);

            if (usuario == null)
            {
                var conditions = new List<ScanCondition>
                {
                    new ScanCondition("Activo", ScanOperator.Equal, true)
                };

                var usuarios = await _dynamoContext.ScanAsync<Usuario>(conditions).GetRemainingAsync();

                foreach (var u in usuarios.Where(u => u.IdTecnico.HasValue))
                {
                    var tecnico = await _dynamoContext.LoadAsync<Tecnico>(u.IdTecnico!.Value);
                    if (tecnico != null && tecnico.Email == request.Usuario)
                    {
                        u.IdTecnicoNavigation = tecnico;
                        usuario = u;
                        break;
                    }
                }
            }
            else if (usuario.IdTecnico.HasValue)
            {
                usuario.IdTecnicoNavigation = await _dynamoContext.LoadAsync<Tecnico>(usuario.IdTecnico.Value);
            }

            if (usuario == null)
                return null;

            bool passwordValida = BCrypt.Net.BCrypt.Verify(
                request.Password,
                usuario.PasswordHash);

            if (!passwordValida)
                return null;

            var token = _tokenService.GenerarToken(usuario);

            return new LoginResponseDto
            {
                Token = token
            };
        }
    }
}