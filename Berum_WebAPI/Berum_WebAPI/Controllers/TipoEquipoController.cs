using Berum_WebAPI.Models;
using Berum_WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Berum_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEquipoController : ControllerBase
    {
        private readonly ITipoEquipoService _service;

        public TipoEquipoController(ITipoEquipoService service)
        {
            _service = service;
        }

        [HttpGet("All")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var TipoEquipo = await _service.GetById(id);
            if (TipoEquipo == null)
            {
                return NotFound();
            }
            return Ok(TipoEquipo);
        }
    }
}
