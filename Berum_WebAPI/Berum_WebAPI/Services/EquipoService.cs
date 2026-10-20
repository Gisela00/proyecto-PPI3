using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Berum_WebAPI.Services
{
    public class EquipoService : IEquipoService
    {
        private readonly ReparacionesDbContext _context;

        public EquipoService(ReparacionesDbContext context)
        {
            _context = context;
        }

        public async Task<List<EquipoDto>> GetAll()
        {
            var equipo = await _context.Equipos.Select(x => new EquipoDto
            {
                modelo = x.Modelo,
                marca = x.IdMarcaNavigation.Nombre,
                TipoEquipo = x.IdTipoEquipoNavigation.Nombre
            }).ToListAsync();
            return equipo;
        }

        public async Task<EquipoDto?> GetById(int id)
        {
            return await _context.Equipos
                .Where(e => e.IdEquipo == id)
                .Select(e => new EquipoDto
                {
                    idEquipo = e.IdEquipo,
                    modelo = e.Modelo,
                    marca = e.IdMarcaNavigation.Nombre,
                    TipoEquipo = e.IdTipoEquipoNavigation.Nombre

                })
                .FirstOrDefaultAsync();
        }

        public async Task<EquipoDto> Create(EquipoCreateDTO dto)
        {
            var equipo = new Equipo
            {
                Modelo = dto.Modelo,
                IdMarca = dto.IdMarca,
                IdTipoEquipo = dto.IdTipoEquipo
            };
            _context.Equipos.Add(equipo);
            _context.SaveChanges();
            var equipoDto = new EquipoDto
            {
                idEquipo = equipo.IdEquipo,
                modelo = equipo.Modelo,
                marca = _context.Marcas.Where(m => m.IdMarca == equipo.IdMarca).Select(m => m.Nombre).FirstOrDefault(),
                TipoEquipo = _context.TipoEquipos.Where(t => t.IdTipoEquipo == equipo.IdTipoEquipo).Select(t => t.Nombre).FirstOrDefault()
            };
            return await GetById(equipo.IdEquipo);
        }
        public async Task<EquipoDto> Update(int id, EquipoCreateDTO dto)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return null;
            }
            equipo.Modelo = dto.Modelo;
            equipo.IdMarca = dto.IdMarca;
            equipo.IdTipoEquipo = dto.IdTipoEquipo;
            _context.Equipos.Update(equipo);
            _context.SaveChanges();
            return await GetById(id);
        }
        public async Task<bool> Delete(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return false;
            }
            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
