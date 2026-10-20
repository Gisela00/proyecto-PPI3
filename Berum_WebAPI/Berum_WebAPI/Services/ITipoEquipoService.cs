using Berum_WebAPI.DTOs;

namespace Berum_WebAPI.Services
{
    public interface ITipoEquipoService
    {
        Task<IEnumerable<TipoEquipoDto>> GetAll();
        Task<TipoEquipoDto?> GetById(int id);
    }
}
