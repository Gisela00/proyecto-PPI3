using Berum_WebAPI.DTOs;

namespace Berum_WebAPI.Services
{
    public interface ITipoFallaService
    {
        Task<IEnumerable<TipoFallaDto>> GetAll();
        Task<TipoFallaDto?> GetById(int id);
    }
}
