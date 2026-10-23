using Amazon.DynamoDBv2.DataModel;
using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;

namespace Berum_WebAPI.Services
{
    public class TipoFallaService : ITipoFallaService
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public TipoFallaService(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task<IEnumerable<TipoFallaDto>> GetAll()
        {
            var tipos = await _dynamoDbContext.ScanAsync<TipoFalla>(new List<ScanCondition>()).GetRemainingAsync();

            return tipos.Select(t => new TipoFallaDto
            {
                IdTipoFalla = t.IdTipoFalla,
                Nombre = t.Nombre
            });
        }

        public async Task<TipoFallaDto?> GetById(int id)
        {
            var t = await _dynamoDbContext.LoadAsync<TipoFalla>(id);
            if (t == null) return null;

            return new TipoFallaDto
            {
                IdTipoFalla = t.IdTipoFalla,
                Nombre = t.Nombre
            };
        }
    }
}