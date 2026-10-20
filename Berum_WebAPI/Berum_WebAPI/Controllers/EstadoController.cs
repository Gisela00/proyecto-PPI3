using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Berum_WebAPI.Services;
using Berum_WebAPI.DTOs;

namespace Berum_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoService _service;
        public EstadoController(IEstadoService service)
        {
            _service = service;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<EstadoDto>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoDto>> GetById(int id)
        {
            var estado = await _service.GetById(id);
            if (estado == null)
            {
                return NotFound();
            }
            return Ok(estado);
        }
    }
}
