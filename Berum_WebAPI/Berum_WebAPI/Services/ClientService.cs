using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Berum_WebAPI.Services
{
    public class ClienteService : IClienteService
{
    private readonly ReparacionesDbContext _context;

    public ClienteService(ReparacionesDbContext context)
    {
        _context = context;
    }
    public async Task<ClienteDTO?> GetById(int id)
    {
            return await _context.Clientes
            .Where(e => e.IdCliente == id)
            .Select(e => new ClienteDTO
            {
                    IdCliente = e.IdCliente,
                    Nombre = e.Nombre,
                    Telefono = e.Telefono,
                    Email = e.Email
            })
                .FirstOrDefaultAsync();
    }
    // Lógica: convertir DTO → Modelo → guardar
    public async Task<ClienteDTO> Create(ClienteCreateDto dto)
    {
        // Validar
        if (string.IsNullOrWhiteSpace(dto.Nombre))
            throw new ArgumentException("El nombre es requerido");

        // Crear modelo
        var cliente = new Cliente
        {
            Nombre = dto.Nombre.Trim(),
            Email = dto.Email,
            Telefono = dto.Telefono
        };

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        // Retornar DTO
        return new ClienteDTO
        {
            IdCliente = cliente.IdCliente,
            Nombre = cliente.Nombre,
            Email = cliente.Email,
            Telefono = cliente.Telefono
        };
    }

    public async Task<List<ClienteDTO>> GetAll()
    {
        return await _context.Clientes
            .Select(c => new ClienteDTO
            {
                IdCliente = c.IdCliente,
                Nombre = c.Nombre,
                Email = c.Email,
                Telefono = c.Telefono
            })
            .ToListAsync();
    }
    public async Task<ClienteDTO> Update(int id, ClienteDTO dto)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
        {
            return null;
        }
        cliente.Nombre = dto.Nombre;
        cliente.Email = dto.Email;
        cliente.Telefono = dto.Telefono;
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
         return await GetById(id);
    }
        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

