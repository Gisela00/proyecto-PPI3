using Amazon.DynamoDBv2.DataModel;
using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;

namespace Berum_WebAPI.Services
{
    public class TipoEquipoService : ITipoEquipoService
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        
        public TipoEquipoService(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task<IEnumerable<TipoEquipoDto>> GetAll()
        {
            var tipos = await _dynamoDbContext.ScanAsync<TipoEquipo>(new List<ScanCondition>()).GetRemainingAsync();

            return tipos.Select(te => new TipoEquipoDto
            {
                IdTipoEquipo = te.IdTipoEquipo,
                Nombre = te.Nombre
            });
        }

        public async Task<TipoEquipoDto?> GetById(int id)
        {
            var te = await _dynamoDbContext.LoadAsync<TipoEquipo>(id);
            if (te == null) return null;

            return new TipoEquipoDto
            {
                IdTipoEquipo = te.IdTipoEquipo,
                Nombre = te.Nombre
            };
        }
    }
}
