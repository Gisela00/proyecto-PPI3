using Berum_WebAPI.Models;
using Berum_WebAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Berum_WebAPI.Services
{
    public class AuthService: IAuthService
    {
        private readonly ReparacionesDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(ReparacionesDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.IdTecnicoNavigation)
                .FirstOrDefaultAsync(u => u.Activo && (u.Usuario1 == request.Usuario 
                          || u.IdTecnicoNavigation != null && u.IdTecnicoNavigation.Email == request.Usuario));

            if (usuario == null)
                return null;

            bool passwordValida =BCrypt.Net.BCrypt.Verify(
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
