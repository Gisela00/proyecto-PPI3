using Berum_WebAPI.DTOs;

namespace Berum_WebAPI.Services
{
    public interface ITecnicoService
    {
    Task<TecnicoDto> Create(TecnicoCreateDTO dto);

    Task<TecnicoDto> Update(int id, TecnicoUpdateDTO dto);
    Task<IEnumerable<TecnicoDto>> GetAll();

    Task<TecnicoDto?> GetById(int id);


    }
}
