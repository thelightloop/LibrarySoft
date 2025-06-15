using Library.Common.ApiResponse;
using Library.Common.DTO;

namespace Library.Services.IssueBook
{
    public interface IBookIssueService
    {
        Task<ApiResponse<string>> AddBookAsync(BookDto book);
        Task<ApiResponse<bool>> UpdateBookAsync(string id, BookDto book);
        Task<ApiResponse<bool>> DeleteBookAsync(string id);
        Task<ApiResponse<IEnumerable<BookDto>>> GetAllBooksAsync();
        Task<ApiResponse<IEnumerable<BookDto>>> SearchBooksAsync(string query);
    }
}
