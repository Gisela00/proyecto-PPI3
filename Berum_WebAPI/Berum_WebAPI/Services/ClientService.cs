using Amazon.DynamoDBv2.DataModel;
using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;

namespace Berum_WebAPI.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public ClienteService(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task<ClienteDTO?> GetById(int id)
        {
            var cliente = await _dynamoDbContext.LoadAsync<Cliente>(id);
            if (cliente == null) return null;

            return new ClienteDTO
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Telefono = cliente.Telefono,
                Email = cliente.Email
            };
        }

        public async Task<ClienteDTO> Create(ClienteCreateDto dto)
        {
            // Validar
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es requerido");

            // DynamoDB no tiene auto-incremento nativo, creamos un ID único numérico basado en timestamp o aleatorio.
            int generatedId = (int)(DateTime.UtcNow.Ticks % int.MaxValue);

            var cliente = new Cliente
            {
                IdCliente = generatedId,
                Nombre = dto.Nombre.Trim(),
                Email = dto.Email,
                Telefono = dto.Telefono
            };

            await _dynamoDbContext.SaveAsync(cliente);

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
            var clientes = await _dynamoDbContext.ScanAsync<Cliente>(new List<ScanCondition>()).GetRemainingAsync();

            return clientes.Select(c => new ClienteDTO
            {
                IdCliente = c.IdCliente,
                Nombre = c.Nombre,
                Email = c.Email,
                Telefono = c.Telefono
            }).ToList();
        }

        public async Task<ClienteDTO?> Update(int id, ClienteDTO dto)
        {
            var cliente = await _dynamoDbContext.LoadAsync<Cliente>(id);
            if (cliente == null)
            {
                return null;
            }

            cliente.Nombre = dto.Nombre;
            cliente.Email = dto.Email;
            cliente.Telefono = dto.Telefono;

            // sobrescribe el ítem existente si la clave de partición coincide
            await _dynamoDbContext.SaveAsync(cliente);

            return await GetById(id);
        }

        public async Task<bool> Delete(int id)
        {
            var cliente = await _dynamoDbContext.LoadAsync<Cliente>(id);
            if (cliente == null)
            {
                return false;
            }

            await _dynamoDbContext.DeleteAsync(cliente);
            return true;
        }
    }
}