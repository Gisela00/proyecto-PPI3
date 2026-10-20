using Berum_WebAPI.DTOs;
using Berum_WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Berum_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ClienteCreateDto dto)
        {
            var resultado = await _clienteService.Create(dto);
            return Ok(resultado);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _clienteService.GetAll();
            return Ok(clientes);
        }
    }
}

