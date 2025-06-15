using Library.Common.ApiResponse;
using Library.Common.DTO;

namespace Library.Services.Fine
{
    public class FineService : IFineService
    {
        public Task<ApiResponse<IEnumerable<FineDto>>> CalculateFinesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<int>> IssueBookAsync(IssueBookDto issue)
        {
            throw new NotImplementedException();
        }
    }
}
