using Berum_WebAPI.DTOs;
namespace Berum_WebAPI.Services
{
    public interface IHistorialEstadoService
    {
        Task<(CreateHistorialResult Result, HistorialEstadoDto? Data)> Create(HistorialEstadoCreateDTO dto);
        Task<IEnumerable<HistorialEstadoDto>> GetAll();
        Task<HistorialEstadoDto?> GetById(int id);
        Task<bool> Update(int id, HistorialEstadoUpdateDTO dto);
    }
}
