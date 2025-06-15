using Library.Common.ApiResponse;
using Library.Common.DTO;

namespace Library.Services.Fine
{
    public interface IFineService
    {
        Task<ApiResponse<IEnumerable<FineDto>>> CalculateFinesAsync();
        Task<ApiResponse<int>> IssueBookAsync(IssueBookDto issue);
       
    }
}
