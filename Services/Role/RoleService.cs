using Library.Common.ApiResponse;

namespace Library.Services.Role
{
    public class RoleService : IRoleService
    {
        Task<ApiResponse<bool>> IRoleService.AssignRoleAsync(int userId, string role)
        {
            throw new NotImplementedException();
        }
    }

}
