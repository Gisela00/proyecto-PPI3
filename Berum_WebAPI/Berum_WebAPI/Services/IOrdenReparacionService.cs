using Berum_WebAPI.DTOs;
using Berum_WebAPI.Models;

namespace Berum_WebAPI.Services
{
    public interface IOrdenReparacionService
    {
        Task<List<OrdenReparacionDTO>> GetAll();
        Task<OrdenReparacionDTO?> GetById(int id);
        Task<OrdenReparacionDTO> Create(OrdenReparacionDTO dto);
        Task<OrdenReparacionDTO?> Update(int id, OrdenReparacionDTO dto);
        Task<bool> Delete(int id);
        Task<OrdenSeguimientoDTO?> GetByCodigo(string numeroServicio); 

    }
}