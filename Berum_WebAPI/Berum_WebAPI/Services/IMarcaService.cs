using Berum_WebAPI.DTOs;

namespace Berum_WebAPI.Services
{
    public interface IMarcaService
    {
        Task<IEnumerable<MarcaDto>> GetAll();
        Task<MarcaDto?> GetById(int id);
    }
}
