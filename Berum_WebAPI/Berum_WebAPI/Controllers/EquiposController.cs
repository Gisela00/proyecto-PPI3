using Berum_WebAPI.DTOs;
using Berum_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Berum_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly IEquipoService _equipoService;

        public EquiposController(IEquipoService equipoService)
        {
            _equipoService = equipoService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var equipos = await _equipoService.GetAll();
            return Ok(equipos);
        }
        [HttpPost("Post")]
        public async Task<IActionResult> Create(EquipoCreateDTO dto)
        {
            var equipo = await _equipoService.Create(dto);
            return Ok(equipo);
        }
        [HttpPut("Update - Put")]
        public async Task<IActionResult> Update(int id, EquipoCreateDTO dto)
        {
            var equipo = await _equipoService.Update(id, dto);
            return Ok(equipo);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _equipoService.Delete(id);
            if (result)
                return Ok();
            else
                return NotFound();

        }
    }
}
