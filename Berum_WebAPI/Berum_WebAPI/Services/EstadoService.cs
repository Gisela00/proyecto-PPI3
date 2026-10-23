using Amazon.DynamoDBv2.DataModel;
using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;

namespace Berum_WebAPI.Services
{
    public class EstadoService : IEstadoService
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public EstadoService(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task<IEnumerable<EstadoDto>> GetAll()
        {
            var estados = await _dynamoDbContext.ScanAsync<Estado>(new List<ScanCondition>()).GetRemainingAsync();

            return estados.Select(e => new EstadoDto
            {
                IdEstado = e.IdEstado,
                Nombre = e.Nombre
            });
        }

        public async Task<EstadoDto?> GetById(int id)
        {
            var e = await _dynamoDbContext.LoadAsync<Estado>(id);
            if (e == null) return null;

            return new EstadoDto 
            {
                IdEstado = e.IdEstado,
                Nombre = e.Nombre
            };
        }
    }
}