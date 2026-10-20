using Berum_WebAPI.DTOs;
using Berum_WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Berum_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly PdfService _pdfService;

        public PdfController(PdfService pdfService)
        {
            _pdfService = pdfService;
        }
        [HttpPost("Generar-Presupuesto")]
        public async Task<IActionResult> GenerarPresupuesto([FromBody] PresupuestoCreateDTO request)
        {
            var model = await _pdfService.ConstruirModeloPresupuesto(request);

            var pdf = _pdfService.GenerarPdf(model);

            return File(
                pdf,
                "application/pdf",
                $"Presupuesto_{model.NumeroServicio}.pdf");
        }
        [HttpPost("Generar-ComprobanteIngreso")]
        public async Task<ActionResult> GenerarComprobanteIngreso([FromBody] ComprobanteIngresoCreateDTO request)
        {
            var model = await _pdfService.ConstruirModeloComprobanteIngreso(request);
            var pdf = _pdfService.GenerarPdfComprobanteIngreso(model);
            return File(
                pdf,
                "application/pdf",
                $"ComprobanteIngreso_{model.NumeroServicio}.pdf");
        }

    }
}
