using Berum_WebAPI.DTOs;
using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace Berum_WebAPI.Services
{
    public class TipoEquipoService : ITipoEquipoService
    {
        private readonly ReparacionesDbContext _context;
        
        public TipoEquipoService(ReparacionesDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TipoEquipoDto>> GetAll()
        {
            return await _context.TipoEquipos
                .Select(te => new TipoEquipoDto
                {
                    IdTipoEquipo = te.IdTipoEquipo,
                    Nombre = te.Nombre
                })
                .ToListAsync();
        }

        public async Task<TipoEquipoDto?> GetById(int id)
        {
            return await _context.TipoEquipos
                .Where(te => te.IdTipoEquipo == id)
                .Select(te => new TipoEquipoDto
                {
                    IdTipoEquipo = te.IdTipoEquipo,
                    Nombre = te.Nombre
                }).FirstOrDefaultAsync();
        }
    }
}
