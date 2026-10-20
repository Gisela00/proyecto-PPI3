using Berum_WebAPI.DTOs;

namespace Berum_WebAPI.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
    }
}
