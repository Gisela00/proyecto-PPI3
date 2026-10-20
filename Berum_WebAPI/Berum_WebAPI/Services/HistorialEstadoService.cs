using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

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
        private readonly ReparacionesDbContext _context;

        public HistorialEstadoService(ReparacionesDbContext context)
        {
            _context = context;
        }

        public async Task<(CreateHistorialResult Result, HistorialEstadoDto? Data)> Create(HistorialEstadoCreateDTO dto)
        {
            var ordenExiste = await _context.OrdenReparacions
                .AnyAsync(o => o.IdOrden == dto.IdOrden);

            if (!ordenExiste)
                return (CreateHistorialResult.OrdenNoExiste, null);

            var estadoExiste = await _context.Estados
                .AnyAsync(e => e.IdEstado == dto.IdEstado);

            if (!estadoExiste)
                return (CreateHistorialResult.EstadoNoExiste, null);

            var ultimoEstado = await _context.HistorialEstados
                .Where(h => h.IdOrden == dto.IdOrden)
                .OrderByDescending(h => h.Fecha)
                .FirstOrDefaultAsync();
           
            if (ultimoEstado != null && ultimoEstado.IdEstado == dto.IdEstado && ultimoEstado.Observaciones == dto.Observaciones)
                return (CreateHistorialResult.Duplicado, null);

            var historialEstado = new HistorialEstado
            {
                IdOrden = dto.IdOrden,
                IdEstado = dto.IdEstado,
                Fecha = DateTime.UtcNow,
                Observaciones = dto.Observaciones
            };

            _context.HistorialEstados.Add(historialEstado);

            var orden = await _context.OrdenReparacions.FindAsync(dto.IdOrden);
            if (orden != null)
            {
                orden.IdEstadoActual = dto.IdEstado;
            }

            await _context.SaveChangesAsync();

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
            var historialEstado = await _context.HistorialEstados.FindAsync(id);

            if (historialEstado == null)
                return false;

            historialEstado.Observaciones = dto.Observaciones;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<HistorialEstadoDto>> GetAll()
        {
            return await _context.HistorialEstados
                .Select(h => new HistorialEstadoDto
                {
                    IdHistorial = h.IdHistorial,
                    IdOrden = h.IdOrden,
                    IdEstado = h.IdEstado,
                    Fecha = h.Fecha,
                    Observaciones = h.Observaciones
                })
                .ToListAsync();
        }

        public async Task<HistorialEstadoDto?> GetById(int id)
        {
            return await _context.HistorialEstados
                .Where(h => h.IdHistorial == id)
                .Select(h => new HistorialEstadoDto
                {
                    IdHistorial = h.IdHistorial,
                    IdOrden = h.IdOrden,
                    IdEstado = h.IdEstado,
                    Fecha = h.Fecha,
                    Observaciones = h.Observaciones
                })
                .FirstOrDefaultAsync();
        }
    }
}