using Berum_WebAPI.DTOs;
using Berum_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Berum_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoFallaController : ControllerBase
    {
        private readonly ITipoFallaService _service;

        public TipoFallaController(ITipoFallaService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tipoFalla = await _service.GetById(id);
            if (tipoFalla == null)
            {
                return NotFound();
            }
            return Ok(tipoFalla);
        }
    }
}
