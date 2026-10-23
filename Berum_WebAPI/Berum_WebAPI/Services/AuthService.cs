using Berum_WebAPI.Models;
using Berum_WebAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Berum_WebAPI.Services
{
    public class AuthService: IAuthService
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
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("Activo", ScanOperator.Equal, true)
            };

            var search = _dynamoContext.ScanAsync<Usuario>(conditions);
            var usuarios = await search.GetRemainingAsync();

            Usuario? usuario = null;

            foreach (var u in usuarios)
            {
                if (u.Usuario1 == request.Usuario)
                {
                    usuario = u;
                    break;
                }

                if (u.IdTecnico.HasValue)
                {
                    var tecnico = await _dynamoContext.LoadAsync<Tecnico>(u.IdTecnico.Value);
                    if (tecnico != null && tecnico.Email == request.Usuario)
                    {
                        u.IdTecnicoNavigation = tecnico; // Asignamos manualmente para el token
                        usuario = u;
                        break;
                    }
                }
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
