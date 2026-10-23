using Amazon.DynamoDBv2.DataModel;
using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;

namespace Berum_WebAPI.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public MarcaService(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task<IEnumerable<MarcaDto>> GetAll()
        {
            var marcas = await _dynamoDbContext.ScanAsync<Marca>(new List<ScanCondition>()).GetRemainingAsync();

            return marcas.Select(m => new MarcaDto
            {
                IdMarca = m.IdMarca,
                Nombre = m.Nombre
            });
        }

        public async Task<MarcaDto?> GetById(int id)
        {
            var marca = await _dynamoDbContext.LoadAsync<Marca>(id);
            if (marca == null) return null;

            return new MarcaDto
            {
                IdMarca = marca.IdMarca,
                Nombre = marca.Nombre
            };
        }
    }
}