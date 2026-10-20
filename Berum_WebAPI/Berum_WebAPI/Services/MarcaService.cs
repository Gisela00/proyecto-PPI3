using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Berum_WebAPI.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly ReparacionesDbContext _context;

        public MarcaService(ReparacionesDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MarcaDto>> GetAll()
        {
            return await  _context.Marcas.Select(m => new MarcaDto
            {
                IdMarca = m.IdMarca,
                Nombre = m.Nombre
            }).ToListAsync();
        }

        public async Task<MarcaDto?> GetById(int id)
        {
            return await _context.Marcas
                .Where(m => m.IdMarca == id)
                .Select(n => new MarcaDto
                {
                    IdMarca = n.IdMarca,
                    Nombre = n.Nombre
                }). FirstOrDefaultAsync();
        }
    }
}
