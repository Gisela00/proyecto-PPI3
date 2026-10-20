using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Berum_WebAPI.Services
{
    public class TipoFallaService : ITipoFallaService
    {
        private readonly ReparacionesDbContext _context;

        public TipoFallaService(ReparacionesDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TipoFallaDto>> GetAll()
        {
            return await _context.TipoFallas
                .Select(t => new TipoFallaDto
                {
                    IdTipoFalla = t.IdTipoFalla,
                    Nombre = t.Nombre
                }).ToListAsync();
        }

        public async Task<TipoFallaDto?> GetById(int id)
        {
            return await _context.TipoFallas
                .Where(t => t.IdTipoFalla == id)
                .Select(t => new TipoFallaDto
                {
                    IdTipoFalla = t.IdTipoFalla,
                    Nombre = t.Nombre
                }).FirstOrDefaultAsync();
        }
    }
}
