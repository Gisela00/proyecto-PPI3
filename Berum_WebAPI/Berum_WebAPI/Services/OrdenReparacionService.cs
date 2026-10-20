using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Berum_WebAPI.Services
{
    public class OrdenReparacionService : IOrdenReparacionService
    {
        private readonly ReparacionesDbContext _context;

        public OrdenReparacionService(ReparacionesDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrdenReparacionDTO>> GetAll()
        {
            return await _context.OrdenReparacions
                .Select(o => new OrdenReparacionDTO
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
                })
                .ToListAsync();
        }

        public async Task<OrdenReparacionDTO?> GetById(int id)
        {
            return await _context.OrdenReparacions
                .Where(o => o.IdOrden == id)
                .Select(o => new OrdenReparacionDTO
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
                })
                .FirstOrDefaultAsync();
        }

        public async Task<OrdenReparacionDTO> Create(OrdenReparacionDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.DescripcionFalla))
                throw new ArgumentException("La descripción de la falla es requerida.");

            if (dto.IdCliente <= 0 || dto.IdEquipo <= 0 || dto.IdEstadoActual <= 0)
                throw new ArgumentException("Los datos de Cliente, Equipo y Estado son obligatorios y deben ser válidos.");

            var orden = new OrdenReparacion
            {
                NumeroServicio = $"ORD-{DateTime.Today:yyyy}-{Guid.NewGuid().ToString()[..4].ToUpper()}",
                FechaIngreso = dto.FechaIngreso,
                IdCliente = dto.IdCliente,
                IdEquipo = dto.IdEquipo,
                IdTecnico = dto.IdTecnico,
                IdEstadoActual = dto.IdEstadoActual,
                IdTipoFalla = dto.IdTipoFalla,
                DescripcionFalla = dto.DescripcionFalla,
                HistorialEstados = new List<HistorialEstado>
                {
                    new HistorialEstado
                    {
                        IdEstado = dto.IdEstadoActual,
                        IdOrden = dto.IdOrden,
                        Fecha = dto.FechaIngreso,
                        Observaciones = "Creación inicial de la orden de reparación."
                    }
                }
            };

            _context.OrdenReparacions.Add(orden);
            await _context.SaveChangesAsync();

            dto.IdOrden = orden.IdOrden;
            dto.NumeroServicio = orden.NumeroServicio;
            return dto;
        }

        public async Task<OrdenReparacionDTO?> Update(int id, OrdenReparacionDTO dto)
        {
            var orden = await _context.OrdenReparacions
                .Include(o => o.HistorialEstados)
                .FirstOrDefaultAsync(o => o.IdOrden == id);

            if (orden == null) return null;

            if (orden.IdEstadoActual != dto.IdEstadoActual)
            {
                orden.HistorialEstados.Add(new HistorialEstado
                {
                    IdEstado = dto.IdEstadoActual,
                    Fecha = DateTime.Now,
                    Observaciones = "La orden cambio de estado."
                });
            }

            orden.NumeroServicio = dto.NumeroServicio;
            orden.FechaIngreso = dto.FechaIngreso;
            orden.IdCliente = dto.IdCliente;
            orden.IdEquipo = dto.IdEquipo;
            orden.IdTecnico = dto.IdTecnico;
            orden.IdEstadoActual = dto.IdEstadoActual;
            orden.IdTipoFalla = dto.IdTipoFalla;
            orden.DescripcionFalla = dto.DescripcionFalla;

            await _context.SaveChangesAsync();

            return await GetById(id);
        }

        public async Task<bool> Delete(int id)
        {
            var orden = await _context.OrdenReparacions.FindAsync(id);
            if (orden == null) return false;

            _context.OrdenReparacions.Remove(orden);
            await _context.SaveChangesAsync();
            return true;
        }
       
        public async Task<OrdenSeguimientoDTO?> GetByCodigo(string codigo)
        {
            return await _context.OrdenReparacions
                .Where(o => o.NumeroServicio == codigo)
                .Select(o => new OrdenSeguimientoDTO
                {
                    NumeroServicio = o.NumeroServicio,
                    DescripcionFalla = o.DescripcionFalla ?? "Sin descripción",
                    TipoEquipo = o.IdEquipoNavigation.IdTipoEquipoNavigation.Nombre,
                    MarcaEquipo = o.IdEquipoNavigation.IdMarcaNavigation.Nombre,
                    ModeloEquipo = o.IdEquipoNavigation.Modelo,
                    TecnicoAsignado = o.IdTecnicoNavigation.Nombre,

                    HistorialEstados = o.HistorialEstados.Select(h => new HistorialEstadoDto
                    {
                        Fecha = h.Fecha,
                        Estado = h.IdEstadoNavigation.Nombre,
                        Observaciones = h.Observaciones ?? "Sin observaciones."
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

    }
}