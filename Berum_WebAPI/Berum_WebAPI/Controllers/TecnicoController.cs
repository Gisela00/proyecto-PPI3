using Berum_WebAPI.DTOs;
using Berum_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Berum_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TecnicoController : ControllerBase
    {
        private readonly ITecnicoService _service;

        public TecnicoController(ITecnicoService service)
        {
            _service = service;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<TecnicoDto>>> GetAll()
        {
            var tecnicos = await _service.GetAll();

            return Ok(tecnicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TecnicoDto>> GetById(int id)
        {
            var tecnico = await _service.GetById(id);

            if (tecnico == null)
            {
                return NotFound();
            }

            return Ok(tecnico);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<TecnicoDto>> Create(TecnicoCreateDTO dto)
        {
            var tecnico = await _service.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = tecnico.IdTecnico },
                tecnico);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TecnicoDto>> Update(
            int id, TecnicoUpdateDTO dto)
        {
            try
            {
                var tecnico = await _service.Update(id, dto);

                return Ok(tecnico);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
