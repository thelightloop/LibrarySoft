using Library.Common.ApiResponse;
using Library.Common.DTO;

namespace Library.Services.IssueBook
{
    public interface IBookIssueService
    {
        Task<ApiResponse<string>> AddBookAsync(BookDto book);
        Task<ApiResponse<bool>> UpdateBookAsync(BookDto book);
        Task<ApiResponse<bool>> DeleteBookAsync(string id);
        Task<ApiResponse<IEnumerable<BookDto>>> GetAllBooksAsync();
        Task<ApiResponse<IEnumerable<IssueBookDto>>> GetAllAssignmentsAsync();
        Task<ApiResponse<string>> AssignBookAsync(IssueBookDto dto);
        Task<bool> ReturnBookAsync(string issueBookId);
    }
}