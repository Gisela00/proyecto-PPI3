using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;

namespace Berum_WebAPI.Services
{
    public enum CreateHistorialResult
    {
        Success,
        OrdenNoExiste,
        EstadoNoExiste,
        Duplicado
    }

    public class HistorialEstadoService : IHistorialEstadoService
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public HistorialEstadoService(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task<(CreateHistorialResult Result, HistorialEstadoDto? Data)> Create(HistorialEstadoCreateDTO dto)
        {
            var orden = await _dynamoDbContext.LoadAsync<OrdenReparacion>(dto.IdOrden);
            if (orden == null)
                return (CreateHistorialResult.OrdenNoExiste, null);

            var estado = await _dynamoDbContext.LoadAsync<Estado>(dto.IdEstado);
            if (estado == null)
                return (CreateHistorialResult.EstadoNoExiste, null);

            
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("IdOrden", ScanOperator.Equal, dto.IdOrden)
            };
            var historialRows = await _dynamoDbContext.ScanAsync<HistorialEstado>(conditions).GetRemainingAsync();
            var ultimoEstado = historialRows.OrderByDescending(h => h.Fecha).FirstOrDefault();
         
            if (ultimoEstado != null && ultimoEstado.IdEstado == dto.IdEstado && ultimoEstado.Observaciones == dto.Observaciones)
                return (CreateHistorialResult.Duplicado, null);

            
            int generatedId = (int)(DateTime.UtcNow.Ticks % int.MaxValue);

            var historialEstado = new HistorialEstado
            {
                IdHistorial = generatedId,
                IdOrden = dto.IdOrden,
                IdEstado = dto.IdEstado,
                Fecha = DateTime.UtcNow,
                Observaciones = dto.Observaciones
            };

            
            await _dynamoDbContext.SaveAsync(historialEstado);

            orden.IdEstadoActual = dto.IdEstado;
            await _dynamoDbContext.SaveAsync(orden);

            return (CreateHistorialResult.Success, new HistorialEstadoDto
            {
                IdHistorial = historialEstado.IdHistorial,
                IdOrden = historialEstado.IdOrden,
                IdEstado = historialEstado.IdEstado,
                Fecha = historialEstado.Fecha,
                Observaciones = historialEstado.Observaciones
            });
        }

        public async Task<bool> Update(int id, HistorialEstadoUpdateDTO dto)
        {
            var historialEstado = await _dynamoDbContext.LoadAsync<HistorialEstado>(id);

            if (historialEstado == null)
                return false;

            historialEstado.Observaciones = dto.Observaciones;

            await _dynamoDbContext.SaveAsync(historialEstado);

            return true;
        }

        public async Task<IEnumerable<HistorialEstadoDto>> GetAll()
        {
            var historiales = await _dynamoDbContext.ScanAsync<HistorialEstado>(new List<ScanCondition>()).GetRemainingAsync();

            return historiales.Select(h => new HistorialEstadoDto
            {
                IdHistorial = h.IdHistorial,
                IdOrden = h.IdOrden,
                IdEstado = h.IdEstado,
                Fecha = h.Fecha,
                Observaciones = h.Observaciones
            });
        }

        public async Task<HistorialEstadoDto?> GetById(int id)
        {
            var h = await _dynamoDbContext.LoadAsync<HistorialEstado>(id);
            if (h == null) return null;

            return new HistorialEstadoDto
            {
                IdHistorial = h.IdHistorial,
                IdOrden = h.IdOrden,
                IdEstado = h.IdEstado,
                Fecha = h.Fecha,
                Observaciones = h.Observaciones
            };
        }
    }
}