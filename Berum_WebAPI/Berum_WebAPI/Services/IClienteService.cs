using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;

namespace Berum_WebAPI.Services
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> GetAll();

        Task<ClienteDTO?> GetById(int id);

        Task<ClienteDTO> Create(ClienteCreateDto dto);

        Task<ClienteDTO?> Update(int id, ClienteDTO dto);

        Task<bool> Delete(int id);
    }
}