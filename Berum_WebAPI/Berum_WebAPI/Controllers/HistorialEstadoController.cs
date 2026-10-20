using Berum_WebAPI.DTOs;
using Berum_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Berum_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialEstadoController : ControllerBase
    {
        private readonly IHistorialEstadoService _service;

        public HistorialEstadoController(IHistorialEstadoService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<HistorialEstadoDto>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialEstadoDto>> GetById(int id)
        {
            var historial = await _service.GetById(id);

            if (historial == null)
                return NotFound();

            return Ok(historial);
        }

        [HttpPost("Post")]
        public async Task<ActionResult<HistorialEstadoDto>> Create([FromBody] HistorialEstadoCreateDTO dto)
        {
            var (result, data) = await _service.Create(dto);

            switch (result)
            {
                case CreateHistorialResult.OrdenNoExiste:
                    return NotFound("La orden no existe.");

                case CreateHistorialResult.EstadoNoExiste:
                    return NotFound("El estado no existe.");

                case CreateHistorialResult.Duplicado:
                    return Conflict("La orden ya se encuentra en ese estado.");

                case CreateHistorialResult.Success:
                    return CreatedAtAction(
                        nameof(GetById),
                        new { id = data!.IdHistorial },
                        data);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HistorialEstadoUpdateDTO dto)
        {
            var actualizado = await _service.Update(id, dto);

            if (!actualizado)
                return NotFound();

            return NoContent();
        }
    }
}