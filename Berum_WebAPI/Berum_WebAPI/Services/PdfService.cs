using Amazon.DynamoDBv2.DataModel;
using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;
using Berum_WebAPI.PDF.ComprobanteIngreso;
using Berum_WebAPI.PDF.Presupuesto;
using QuestPDF.Fluent;

namespace Berum_WebAPI.Services
{
    public class PdfService
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public PdfService(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
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

            var o = await _dynamoDbContext.LoadAsync<OrdenReparacion>(idOrden);
            if (o == null)
                throw new Exception("Orden no encontrada");

            var cliente = await _dynamoDbContext.LoadAsync<Cliente>(o.IdCliente);
            var equipo = await _dynamoDbContext.LoadAsync<Equipo>(o.IdEquipo);

            Marca? marca = null;
            TipoEquipo? tipoEquipo = null;

            if (equipo != null)
            {
                marca = await _dynamoDbContext.LoadAsync<Marca>(equipo.IdMarca);
                tipoEquipo = await _dynamoDbContext.LoadAsync<TipoEquipo>(equipo.IdTipoEquipo);
            }

            return new DatosOrdenPdfDTO
            {
                IdOrden = o.IdOrden,
                NumeroServicio = o.NumeroServicio,
                FechaIngreso = o.FechaIngreso,
                Cliente = cliente?.Nombre ?? "Desconocido",
                Telefono = cliente?.Telefono,
                Email = cliente?.Email,
                TipoEquipo = tipoEquipo?.Nombre ?? "Desconocido",
                Marca = marca?.Nombre ?? "Desconocida",
                Modelo = equipo?.Modelo ?? "Desconocido",
                DescripcionFalla = o.DescripcionFalla ?? "Sin descripción"
            };
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