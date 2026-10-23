using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;

namespace Berum_WebAPI.Services
{
    public class OrdenReparacionService : IOrdenReparacionService
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public OrdenReparacionService(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task<List<OrdenReparacionDTO>> GetAll()
        {
            var ordenes = await _dynamoDbContext.ScanAsync<OrdenReparacion>(new List<ScanCondition>()).GetRemainingAsync();

            return ordenes.Select(o => new OrdenReparacionDTO
            {
                IdOrden = o.IdOrden,
                NumeroServicio = o.NumeroServicio,
                FechaIngreso = o.FechaIngreso,
                IdCliente = o.IdCliente,
                IdEquipo = o.IdEquipo,
                IdTecnico = o.IdTecnico,
                IdEstadoActual = o.IdEstadoActual,
                IdTipoFalla = o.IdTipoFalla,
                DescripcionFalla = o.DescripcionFalla
            }).ToList();
        }

        public async Task<OrdenReparacionDTO?> GetById(int id)
        {
            var o = await _dynamoDbContext.LoadAsync<OrdenReparacion>(id);
            if (o == null) return null;

            return new OrdenReparacionDTO
            {
                IdOrden = o.IdOrden,
                NumeroServicio = o.NumeroServicio,
                FechaIngreso = o.FechaIngreso,
                IdCliente = o.IdCliente,
                IdEquipo = o.IdEquipo,
                IdTecnico = o.IdTecnico,
                IdEstadoActual = o.IdEstadoActual,
                IdTipoFalla = o.IdTipoFalla,
                DescripcionFalla = o.DescripcionFalla
            };
        }

        public async Task<OrdenReparacionDTO> Create(OrdenReparacionDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.DescripcionFalla))
                throw new ArgumentException("La descripción de la falla es requerida.");

            if (dto.IdCliente <= 0 || dto.IdEquipo <= 0 || dto.IdEstadoActual <= 0)
                throw new ArgumentException("Los datos de Cliente, Equipo y Estado son obligatorios y deben ser válidos.");

            
            int generatedId = (int)(DateTime.UtcNow.Ticks % int.MaxValue);
            string numeroServicio = $"ORD-{DateTime.Today:yyyy}-{Guid.NewGuid().ToString()[..4].ToUpper()}";

            var orden = new OrdenReparacion
            {
                IdOrden = generatedId,
                NumeroServicio = numeroServicio,
                FechaIngreso = dto.FechaIngreso == default ? DateTime.UtcNow : dto.FechaIngreso,
                IdCliente = dto.IdCliente,
                IdEquipo = dto.IdEquipo,
                IdTecnico = dto.IdTecnico,
                IdEstadoActual = dto.IdEstadoActual,
                IdTipoFalla = dto.IdTipoFalla,
                DescripcionFalla = dto.DescripcionFalla
            };

            await _dynamoDbContext.SaveAsync(orden);

        
            int historyId = (int)(DateTime.UtcNow.Ticks % int.MaxValue);
            var historialInicial = new HistorialEstado
            {
                IdHistorial = historyId,
                IdOrden = generatedId,
                IdEstado = dto.IdEstadoActual,
                Fecha = orden.FechaIngreso,
                Observaciones = "Creación inicial de la orden de reparación."
            };
            await _dynamoDbContext.SaveAsync(historialInicial);

            dto.IdOrden = generatedId;
            dto.NumeroServicio = numeroServicio;
            return dto;
        }

        public async Task<OrdenReparacionDTO?> Update(int id, OrdenReparacionDTO dto)
        {
            var orden = await _dynamoDbContext.LoadAsync<OrdenReparacion>(id);
            if (orden == null) return null;

        
            if (orden.IdEstadoActual != dto.IdEstadoActual)
            {
                int historyId = (int)(DateTime.UtcNow.Ticks % int.MaxValue);
                var historialCambio = new HistorialEstado
                {
                    IdHistorial = historyId,
                    IdOrden = id,
                    IdEstado = dto.IdEstadoActual,
                    Fecha = DateTime.UtcNow,
                    Observaciones = "La orden cambió de estado."
                };
                await _dynamoDbContext.SaveAsync(historialCambio);
            }

            orden.NumeroServicio = dto.NumeroServicio;
            orden.FechaIngreso = dto.FechaIngreso;
            orden.IdCliente = dto.IdCliente;
            orden.IdEquipo = dto.IdEquipo;
            orden.IdTecnico = dto.IdTecnico;
            orden.IdEstadoActual = dto.IdEstadoActual;
            orden.IdTipoFalla = dto.IdTipoFalla;
            orden.DescripcionFalla = dto.DescripcionFalla;

            await _dynamoDbContext.SaveAsync(orden);

            return await GetById(id);
        }

        public async Task<bool> Delete(int id)
        {
            var orden = await _dynamoDbContext.LoadAsync<OrdenReparacion>(id);
            if (orden == null) return false;

            await _dynamoDbContext.DeleteAsync(orden);
            return true;
        }
       
        public async Task<OrdenSeguimientoDTO?> GetByCodigo(string codigo)
        {
           
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("NumeroServicio", ScanOperator.Equal, codigo)
            };
            var ordenes = await _dynamoDbContext.ScanAsync<OrdenReparacion>(conditions).GetRemainingAsync();
            var o = ordenes.FirstOrDefault();

            if (o == null) return null;

            
            var equipo = await _dynamoDbContext.LoadAsync<Equipo>(o.IdEquipo);
            Marca? marca = null;
            TipoEquipo? tipoEquipo = null;

            if (equipo != null)
            {
                marca = await _dynamoDbContext.LoadAsync<Marca>(equipo.IdMarca);
                tipoEquipo = await _dynamoDbContext.LoadAsync<TipoEquipo>(equipo.IdTipoEquipo);
            }

            Tecnico? tecnico = null;
            if (o.IdTecnico.HasValue)
            {
                tecnico = await _dynamoDbContext.LoadAsync<Tecnico>(o.IdTecnico.Value);
            }

           
            var historyConditions = new List<ScanCondition>
            {
                new ScanCondition("IdOrden", ScanOperator.Equal, o.IdOrden)
            };
            var historiales = await _dynamoDbContext.ScanAsync<HistorialEstado>(historyConditions).GetRemainingAsync();

            var historialDtos = new List<HistorialEstadoDto>();
            foreach (var h in historiales.OrderBy(x => x.Fecha))
            {
                var estado = await _dynamoDbContext.LoadAsync<Estado>(h.IdEstado);
                historialDtos.Add(new HistorialEstadoDto
                {
                    Fecha = h.Fecha,
                    Estado = estado?.Nombre ?? "Desconocido",
                    Observaciones = h.Observaciones ?? "Sin observaciones."
                });
            }

            return new OrdenSeguimientoDTO
            {
                NumeroServicio = o.NumeroServicio,
                DescripcionFalla = o.DescripcionFalla ?? "Sin descripción",
                TipoEquipo = tipoEquipo?.Nombre ?? "Desconocido",
                MarcaEquipo = marca?.Nombre ?? "Desconocida",
                ModeloEquipo = equipo?.Modelo ?? "Desconocido",
                TecnicoAsignado = tecnico?.Nombre ?? "No asignado",
                HistorialEstados = historialDtos
            };
        }
    }
}