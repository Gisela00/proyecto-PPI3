using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;
using Berum_WebAPI.PDF.ComprobanteIngreso;
using Berum_WebAPI.PDF.Presupuesto;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;

namespace Berum_WebAPI.Services
{
    public class PdfService
    {
        private readonly ReparacionesDbContext _context;
        public PdfService(ReparacionesDbContext context)
        {
            _context = context;
        }
        public byte[] GenerarPdf(PresupuestoModel request)
        {
            var document = new PresupuestoDocument(request);
            return document.GeneratePdf();
        }
        public byte[] GenerarPdfComprobanteIngreso(ComprobanteIngresoModel request)
        {
            var document = new ComprobanteIngresoDocument(request);
            return document.GeneratePdf();
        }
        public async Task<DatosOrdenPdfDTO> ObtenerDatosOrdenPdf(int idOrden)
        {
            var datos = await _context.OrdenReparacions
                .Where(o => o.IdOrden == idOrden)
                .Select(o => new DatosOrdenPdfDTO
                {
                    IdOrden = o.IdOrden,
                    NumeroServicio = o.NumeroServicio,
                    FechaIngreso = o.FechaIngreso,
                    Cliente = o.IdClienteNavigation.Nombre,
                    Telefono = o.IdClienteNavigation.Telefono,
                    Email = o.IdClienteNavigation.Email,
                    TipoEquipo = o.IdEquipoNavigation.IdTipoEquipoNavigation.Nombre,
                    Marca = o.IdEquipoNavigation.IdMarcaNavigation.Nombre,
                    Modelo = o.IdEquipoNavigation.Modelo,
                    DescripcionFalla = o.DescripcionFalla
                })
                .FirstOrDefaultAsync();

            if (datos == null)
                throw new Exception("Orden no encontrada");

            return datos;
        }
        public async Task<PresupuestoModel> ConstruirModeloPresupuesto(PresupuestoCreateDTO request)
        {
            var datos = await ObtenerDatosOrdenPdf(request.IdOrden);

            return new PresupuestoModel
            {
                IdOrden = datos.IdOrden,

                NumeroServicio = datos.NumeroServicio,

                FechaEmision = DateTime.Now,

                EstadoPresupuesto = EstadoPresupuesto.Pendiente,

                Monto = request.Monto,

                TipoEquipo = datos.TipoEquipo,

                Marca = datos.Marca,

                Modelo = datos.Modelo,

                FallasDetectadas = new List<string>
        {
            datos.DescripcionFalla
        },

                DescripcionTrabajo = request.DescripcionTrabajo,

                ObservacionesTecnico = request.ObservacionesTecnico,

                Cliente = new ClienteDTO
                {
                    Nombre = datos.Cliente,
                    Telefono = datos.Telefono,
                    Email = datos.Email
                }
            };
        }
        public async Task<ComprobanteIngresoModel> ConstruirModeloComprobanteIngreso(ComprobanteIngresoCreateDTO request)
        {
            var datos = await ObtenerDatosOrdenPdf(request.IdOrden);

            return new ComprobanteIngresoModel
            {
                IdOrden = datos.IdOrden,

                NumeroServicio = datos.NumeroServicio,

                FechaIngreso = datos.FechaIngreso,

                TipoEquipo = datos.TipoEquipo,

                Marca = datos.Marca,

                Modelo = datos.Modelo,

                ProblemaInformado = datos.DescripcionFalla,

                AccesoriosEntregados = request.AccesoriosEntregados,

                EstadoInicial = request.EstadoInicial,

                Cliente = new ClienteDTO
                {
                    Nombre = datos.Cliente,
                    Telefono = datos.Telefono,
                    Email = datos.Email
                }
            };
        }
    }
}
