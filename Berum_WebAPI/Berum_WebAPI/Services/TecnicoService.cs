using Amazon.DynamoDBv2.DataModel;
using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;

namespace Berum_WebAPI.Services
{
    public class TecnicoService : ITecnicoService
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public TecnicoService(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task<TecnicoDto> Create(TecnicoCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es requerido");

            int generatedId = (int)(DateTime.UtcNow.Ticks % int.MaxValue);

            var tecnico = new Tecnico
            {
                IdTecnico = generatedId,
                Nombre = dto.Nombre.Trim(),
                Telefono = dto.Telefono,
                Especialidad = dto.Especialidad,
                Email = dto.Email,
            };
            
            await _dynamoDbContext.SaveAsync(tecnico);

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
            var tecnico = await _dynamoDbContext.LoadAsync<Tecnico>(id);
            if (tecnico == null)
            {
                throw new KeyNotFoundException("El técnico no existe.");
            }

            tecnico.Nombre = dto.Nombre;
            tecnico.Telefono = dto.Telefono;
            tecnico.Especialidad = dto.Especialidad;
            tecnico.Email = dto.Email;

            await _dynamoDbContext.SaveAsync(tecnico);

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
            var tecnicos = await _dynamoDbContext.ScanAsync<Tecnico>(new List<ScanCondition>()).GetRemainingAsync();

            return tecnicos.Select(t => new TecnicoDto
            {
                IdTecnico = t.IdTecnico,
                Nombre = t.Nombre,
                Telefono = t.Telefono,
                Especialidad = t.Especialidad,
                Email = t.Email
            });
        }

        public async Task<TecnicoDto?> GetById(int id)
        {
            var t = await _dynamoDbContext.LoadAsync<Tecnico>(id);
            if (t == null) return null;

            return new TecnicoDto
            {
                IdTecnico = t.IdTecnico,
                Nombre = t.Nombre,
                Telefono = t.Telefono,
                Especialidad = t.Especialidad,
                Email = t.Email
            };
        }
    }
}