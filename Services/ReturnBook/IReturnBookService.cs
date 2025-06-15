using Library.Common.ApiResponse;
using Library.Common.DTO;

namespace Library.Services.ReturnBook
{
    public interface IReturnBookService
    {
        Task<ApiResponse<bool>> ReturnBookAsync(ReturnBookDto returnDto);
        Task<ApiResponse<IEnumerable<IssueBookDto>>> GetIssueHistoryAsync();
    }
}
