using Berum_WebAPI.Models;
 
namespace Berum_WebAPI.Services
{
    public interface ITokenService
    {
        string GenerarToken(Usuario usuario);
    }
}