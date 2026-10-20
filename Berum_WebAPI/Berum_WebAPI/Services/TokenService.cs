using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Berum_WebAPI.Models;
using Microsoft.IdentityModel.Tokens;

    namespace Berum_WebAPI.Services
    {
        public class TokenService : ITokenService
        {
            private readonly IConfiguration _configuration;
    
            public TokenService(IConfiguration configuration)
            {
                _configuration = configuration;
            }
    
            public string GenerarToken(Usuario usuario)
            {
                var jwtSettings = _configuration.GetSection("Jwt");
                var key = jwtSettings["Key"]!;
                var issuer = jwtSettings["Issuer"];
                var audience = jwtSettings["Audience"];
                var expiresInMinutes = int.Parse(jwtSettings["ExpiresInMinutes"] ?? "60");
    
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, usuario.Usuario1),
                    new Claim(ClaimTypes.GivenName, usuario.Nombre),
                    new Claim(ClaimTypes.Role, usuario.Rol)
                };
    
                if (usuario.IdTecnico.HasValue)
                {
                    claims.Add(new Claim("idTecnico", usuario.IdTecnico.Value.ToString()));
                }
    
                var keyBytes = Encoding.UTF8.GetBytes(key);
                var credentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256);
    
                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                    signingCredentials: credentials);
    
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }