using Library.Common.ApiResponse;
using Library.Common.DTO;

namespace Library.Services.Auth
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> RegisterAsync(RegisterDto dto);
        Task<ApiResponse<string>> LoginAsync(LoginDto dto);
    }
}
