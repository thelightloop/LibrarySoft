using Library.Common.ApiResponse;
using Library.Common.DTO;
using Library.Database;
using Library.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.IssueBook
{
    public class BookIssueService(AppDbContext context) : IBookIssueService
    {
        public async Task<ApiResponse<IEnumerable<BookDto>>> GetAllBooksAsync()
        {
            var books = await context.Books.ToListAsync();

            var bookDtos = books.Select(book => new BookDto
            {
                Title = book.Title,
                Id = book.Id.ToString(),
                Author = book.Author,
                ISBN = book.ISBN,
                Category = book.Category,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies
            });

            return new ApiResponse<IEnumerable<BookDto>>(
                success: true,
                message: "Books retrieved successfully.",
                data: bookDtos,
                statusCode: 200
            );
        }

        public Task<ApiResponse<string>> AddBookAsync(BookDto dto)
        {
            Book book = new()
            {
                Title = dto.Title,
                Author = dto.Author,
                ISBN = dto.ISBN,
                CreatedBy = "Admin",
                Category = dto.Category ?? "ALL",
                TotalCopies = dto.TotalCopies?? 0,
                AvailableCopies = dto.TotalCopies??0
            };

            context.Books.Add(book);
            context.SaveChanges();
            ApiResponse<string> response = new(
            success: true,
            message: "Book added successfully.",
            data: book.Title,
            statusCode: 201
                );

            return Task.FromResult(response);
        }



        public async Task<ApiResponse<bool>> DeleteBookAsync(string id)
        {

            var book = await context.Books.Where(a => a.Id == id).FirstOrDefaultAsync();

            if (book.Id != null)
            {
                context.Books.Remove(book);
                await context.SaveChangesAsync();

                return new ApiResponse<bool>(
                    success: true,
                    message: "Book deleted successfully.",
                    statusCode: 200
                );
            }

            return new ApiResponse<bool>(
                    success: false,
                    message: "Book not found.",
                    statusCode: 404
                );
        }




        public Task<ApiResponse<IEnumerable<BookDto>>> SearchBooksAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> UpdateBookAsync(string id, BookDto book)
        {
            throw new NotImplementedException();
        }




    }

}
