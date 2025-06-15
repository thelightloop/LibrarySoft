using Library.Common.ApiResponse;
using Library.Common.DTO;

namespace Library.Services.ReturnBook
{
    public class ReturnBookService : IReturnBookService
    {
        public Task<ApiResponse<IEnumerable<IssueBookDto>>> GetIssueHistoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> ReturnBookAsync(ReturnBookDto returnDto)
        {
            throw new NotImplementedException();
        }
    }
}
