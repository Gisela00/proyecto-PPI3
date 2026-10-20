using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;

namespace Berum_WebAPI.Services
{
    public interface IEquipoService
    {
        Task<List<EquipoDto>> GetAll();

        Task<EquipoDto?> GetById(int id);

        Task<EquipoDto> Create(EquipoCreateDTO dto);

        Task<EquipoDto> Update(int id, EquipoCreateDTO dto);

        Task<bool> Delete(int id);
    }
}
