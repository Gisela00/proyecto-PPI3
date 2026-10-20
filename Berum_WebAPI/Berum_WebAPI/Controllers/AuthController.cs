using Berum_WebAPI.DTOs;
using Berum_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Berum_WebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>>Login([FromBody]LoginRequestDto request)
        {
            var result =await _authService.LoginAsync(request);

            if (result == null)
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos." });

            return Ok(result);
        }
    }
}
