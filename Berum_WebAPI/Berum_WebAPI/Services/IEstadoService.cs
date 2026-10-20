using Berum_WebAPI.DTOs;

namespace Berum_WebAPI.Services
{
    public interface IEstadoService
    {
        Task<IEnumerable<EstadoDto>> GetAll();
        Task<EstadoDto?> GetById(int id);
    }
}
