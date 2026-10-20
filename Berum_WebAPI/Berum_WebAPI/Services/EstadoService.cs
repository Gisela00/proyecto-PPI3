using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
namespace Berum_WebAPI.Services
{
    public class EstadoService : IEstadoService
    {
        private readonly ReparacionesDbContext _context;

        public EstadoService(ReparacionesDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EstadoDto>> GetAll()
        {
            return await _context.Estados.Select(e => new EstadoDto
            {
                IdEstado = e.IdEstado,
                Nombre = e.Nombre
            }).ToListAsync();
        }

        public async Task<EstadoDto?> GetById(int id)
        {
            return await _context.Estados
                .Where(e => e.IdEstado == id)
                .Select(e => new EstadoDto 
                {
                    IdEstado = e.IdEstado,
                    Nombre = e.Nombre
                
                }).FirstOrDefaultAsync();
        }
    }
}
