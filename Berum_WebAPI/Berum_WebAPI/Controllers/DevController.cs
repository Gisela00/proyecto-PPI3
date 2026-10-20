using Microsoft.AspNetCore.Mvc;
 
namespace Berum_WebAPI.Controllers
{
    // TEMPORAL — Voy a borrar este controller una vez podamos cerrar las pruebas pertinentes.
    [ApiController]
    [Route("api/[controller]")]
    public class DevController : ControllerBase
    {
        [HttpGet("hash")]
        public IActionResult GenerarHash([FromQuery] string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            return Ok(new { password, hash });
        }

        [HttpGet("verify")]
        public IActionResult VerificarHash([FromQuery] string password, [FromQuery] string hash)
        {
            bool esValido = BCrypt.Net.BCrypt.Verify(password, hash);
            return Ok(new { password, hash, esValido });
        }
    }
}