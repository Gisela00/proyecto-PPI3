using Amazon.DynamoDBv2.DataModel;
using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;

namespace Berum_WebAPI.Services
{
    public class EquipoService : IEquipoService
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public EquipoService(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task<List<EquipoDto>> GetAll()
        {
            var equipos = await _dynamoDbContext.ScanAsync<Equipo>(new List<ScanCondition>()).GetRemainingAsync();
            var resultados = new List<EquipoDto>();

            foreach (var x in equipos)
            {
                var marca = await _dynamoDbContext.LoadAsync<Marca>(x.IdMarca);
                var tipoEquipo = await _dynamoDbContext.LoadAsync<TipoEquipo>(x.IdTipoEquipo);

                resultados.Add(new EquipoDto
                {
                    idEquipo = x.IdEquipo,
                    modelo = x.Modelo,
                    marca = marca?.Nombre ?? string.Empty,
                    TipoEquipo = tipoEquipo?.Nombre ?? string.Empty
                });
            }

            return resultados;
        }

        public async Task<EquipoDto?> GetById(int id)
        {
            var e = await _dynamoDbContext.LoadAsync<Equipo>(id);
            if (e == null) return null;

            var marca = await _dynamoDbContext.LoadAsync<Marca>(e.IdMarca);
            var tipoEquipo = await _dynamoDbContext.LoadAsync<TipoEquipo>(e.IdTipoEquipo);

            return new EquipoDto
            {
                idEquipo = e.IdEquipo,
                modelo = e.Modelo,
                marca = marca?.Nombre ?? string.Empty,
                TipoEquipo = tipoEquipo?.Nombre ?? string.Empty
            };
        }

        public async Task<EquipoDto> Create(EquipoCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Modelo))
                throw new ArgumentException("El modelo es requerido");

            int generatedId = (int)(DateTime.UtcNow.Ticks % int.MaxValue);

            var equipo = new Equipo
            {
                IdEquipo = generatedId,
                Modelo = dto.Modelo.Trim(),
                IdMarca = dto.IdMarca,
                IdTipoEquipo = dto.IdTipoEquipo
            };

            await _dynamoDbContext.SaveAsync(equipo);

            return await GetById(equipo.IdEquipo);
        }

        public async Task<EquipoDto?> Update(int id, EquipoCreateDTO dto)
        {
            var equipo = await _dynamoDbContext.LoadAsync<Equipo>(id);
            if (equipo == null)
            {
                return null;
            }

            equipo.Modelo = dto.Modelo;
            equipo.IdMarca = dto.IdMarca;
            equipo.IdTipoEquipo = dto.IdTipoEquipo;

            await _dynamoDbContext.SaveAsync(equipo);

            return await GetById(id);
        }

        public async Task<bool> Delete(int id)
        {
            var equipo = await _dynamoDbContext.LoadAsync<Equipo>(id);
            if (equipo == null)
            {
                return false;
            }

            await _dynamoDbContext.DeleteAsync(equipo);
            return true;
        }
    }
}