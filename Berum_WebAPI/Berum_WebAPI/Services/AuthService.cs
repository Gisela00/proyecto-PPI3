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
            Usuario? usuario = null;

            var porUsuario = await _dynamoContext
                .QueryAsync<Usuario>(request.Usuario, new QueryConfig { IndexName = "Usuario1-index" })
                .GetRemainingAsync();

            usuario = porUsuario.FirstOrDefault(u => u.Activo);

            if (usuario == null)
            {
                var porEmail = await _dynamoContext
                    .QueryAsync<Usuario>(request.Usuario, new QueryConfig { IndexName = "Email-index" })
                    .GetRemainingAsync();

                usuario = porEmail.FirstOrDefault(u => u.Activo);
            }

            if (usuario == null)
                return null;

            if (usuario.IdTecnico.HasValue)
            {
                usuario.IdTecnicoNavigation = await _dynamoContext.LoadAsync<Tecnico>(usuario.IdTecnico.Value);
            }

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