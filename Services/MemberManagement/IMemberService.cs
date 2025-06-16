using Library.Common.ApiResponse;
using Library.Common.DTO;

namespace Library.Services.MemberManagement
{

    public interface IMemberService
    {
        Task<ApiResponse<string>> AddMemberAsync(MemberDto member);
        Task<ApiResponse<bool>> UpdateMemberAsync(MemberDto member);
        Task<ApiResponse<IEnumerable<MemberDto>>> GetAllMembersAsync();
        Task<ApiResponse<MemberDto>> GetByIdAsync(string id);
        Task<ApiResponse<bool>> DeleteAsync(string id);
    }
}

    
