using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Berum_WebAPI.Services
{
    public class TecnicoService : ITecnicoService
    {
        private readonly ReparacionesDbContext _context;
        public TecnicoService(ReparacionesDbContext context)
        {
            _context = context;
        }
        public async Task<TecnicoDto> Create(TecnicoCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es requerido");

            var tecnico = new Tecnico
            {
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Especialidad = dto.Especialidad,
                Email = dto.Email,
            };
            
            _context.Tecnicos.Add(tecnico);
            await _context.SaveChangesAsync();

            return new TecnicoDto
            {
                IdTecnico = tecnico.IdTecnico,
                Nombre = tecnico.Nombre,
                Telefono = tecnico.Telefono,
                Especialidad = tecnico.Especialidad,
                Email = tecnico.Email
            };
        }


        public async Task<TecnicoDto> Update(int id, TecnicoUpdateDTO dto)
        {
            var tecnico = await _context.Tecnicos
                .FirstOrDefaultAsync(t => t.IdTecnico == id);
            if (tecnico == null)
            {
                throw new KeyNotFoundException("El técnico no existe.");
            }
            tecnico.Nombre = dto.Nombre;
            tecnico.Telefono = dto.Telefono;
            tecnico.Especialidad = dto.Especialidad;
            tecnico.Email = dto.Email;

            await _context.SaveChangesAsync();
            return new TecnicoDto
            {
                IdTecnico = tecnico.IdTecnico,
                Nombre = tecnico.Nombre,
                Telefono = tecnico.Telefono,
                Especialidad = tecnico.Especialidad,
                Email = tecnico.Email
            };

        }
        public async Task<IEnumerable<TecnicoDto>> GetAll()
        {
            return await _context.Tecnicos.Select(t => new TecnicoDto
            {
                IdTecnico = t.IdTecnico,
                Nombre = t.Nombre,
                Telefono = t.Telefono,
                Especialidad = t.Especialidad,
                Email = t.Email
            })
            .ToListAsync();
        }
        public async Task<TecnicoDto?> GetById(int id)
        {
            return await _context.Tecnicos
                .Where(t => t.IdTecnico == id)
                .Select(t => new TecnicoDto
                {
                    IdTecnico = t.IdTecnico,
                    Nombre = t.Nombre,
                    Telefono = t.Telefono,
                    Especialidad = t.Especialidad,
                    Email = t.Email
                })
                .FirstOrDefaultAsync();
        }

    }
}
