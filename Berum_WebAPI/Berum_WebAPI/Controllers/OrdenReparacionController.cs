using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;
using Berum_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Berum_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenReparacion : ControllerBase
    {
        private readonly IOrdenReparacionService _ordenReparacionService;
        private readonly IEmailService _emailService;
        private readonly IClienteService _clienteService;

        public OrdenReparacion(IOrdenReparacionService ordenReparacionService, IEmailService emailService, IClienteService clienteService)
        {
            _ordenReparacionService = ordenReparacionService;
            _emailService = emailService;
            _clienteService = clienteService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(OrdenReparacionDTO dto)
        {
            var resultado = await _ordenReparacionService.Create(dto);

            //Debug.WriteLine($"IdCliente recibido: {resultado.IdCliente}");

            var cliente = await _clienteService.GetById(dto.IdCliente);
            await _emailService.SendEmail(cliente.Email,"Recepci�n de equipo para evaluaci�n t�cnica",$@"<h2>Equipo recibido</h2>

            <p>Estimado/a {cliente.Nombre}:</p>

            <p>Hemos recibido su equipo para evaluaci�n t�cnica.</p>

            <p>
                <b>N�mero de servicio:</b> {resultado.NumeroServicio}
            </p>

            <p>Nos comunicaremos con usted una vez finalizada la revisi�n.</p>
            ");
            return Ok(resultado);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var ordenReparacion = await _ordenReparacionService.GetAll();

            return Ok(ordenReparacion);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, OrdenReparacionDTO dto)
        {
            var estado = "";
            var cliente = await _clienteService.GetById(dto.IdCliente);
            var ordenReparacion = await _ordenReparacionService.Update(id, dto);

            switch (dto.IdEstadoActual)
            {
                case 3:
                    estado = "El equipo ya se encuentra en buenas manos para efectuar la reparaci�n.";
                    break;
                case 4:
                    estado = " El equipo se encuentra esperando repuestos del proveedor.";
                        break;
                case 5:
                    estado = " El equipo ya est� reparado!";
                    break;
                case 6:
                    estado = " Su equipo ha sido entregado!";
                    break;
                case 7:
                    estado = " La orden ha sido cancelada!.";
                    break;
            }


            await _emailService.SendEmail(cliente.Email, "Novedades sobre su equipo", $@"<h2>Actualizaci�n de estado de la orden de reparaci�n</h2>

            <p>Estimado/a {cliente.Nombre}:</p>

            <p>Le informamos que el estado de su orden de reparaci�n ha sido actualizada: {estado}</p>

            <p>
                <b>N�mero de servicio:</b> {dto.NumeroServicio}
            </p>

            <p>Ante cualquier consulta, no dude en contactarnos o visitar nuestro sitio web www.berumrepairshop.com</p>
            ");
            return Ok(ordenReparacion);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ordenReparacionService.Delete(id);
            if (result)
                return Ok();
            else
                return NotFound();
        }
        [HttpGet("GetByCodigo/{codigo}")]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return BadRequest("El código de orden es requerido.");

            var orden = await _ordenReparacionService.GetByCodigo(codigo);

            if (orden == null)
                return NotFound($"No se encontró ninguna orden con el código {codigo}.");
             
            return Ok(orden);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orden = await _ordenReparacionService.GetById(id);
            if (orden == null)
                return NotFound($"No se encontró la orden con ID {id}.");

            return Ok(orden);
        }

    }
    
}
