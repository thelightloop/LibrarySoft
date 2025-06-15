using Library.Common.ApiResponse;

namespace Library.Services.Role
{
    public interface IRoleService
    {
        Task<ApiResponse<bool>> AssignRoleAsync(int userId, string role);
    }
}
