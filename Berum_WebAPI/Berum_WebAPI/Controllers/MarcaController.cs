using Berum_WebAPI.DTOs;
using Berum_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Berum_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _service;
        public MarcaController(IMarcaService service)
        {
            _service = service;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<MarcaDto>>> GetAll()
        {
            var marcas = await _service.GetAll();
            return Ok(marcas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarcaDto>> GetById(int id)
        {
            var marca = await _service.GetById(id);
            if (marca == null)
            {
                return NotFound();
            }
            return Ok(marca);
        }
    }
}
