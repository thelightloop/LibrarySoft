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
            List<Book> books = await context.Books.ToListAsync();

            IEnumerable<BookDto> bookDtos = books.Select(book => new BookDto
            {
                Title = book.Title,
                Id = book.Id.ToString(),
                Author = book.Author,
                ISBN = book.ISBN,
                Category = book.Category,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies
            });

            return ApiResponse<IEnumerable<BookDto>>.SuccessResponse(bookDtos, "Books retrieved successfully.");
        }

        public async Task<ApiResponse<IEnumerable<IssueBookDto>>> GetAllAssignmentsAsync()
        {
            var books = await context.IssueBooks.Include(issueBook => issueBook.Book)
                .Include(issueBook => issueBook.Member).ToListAsync();

            var bookDtos = books.Select(book => new IssueBookDto()
            {
                Member = book.Member, Book = book.Book, DueDate = book.DueDate, IssueDate = book.IssueDate, Id = book.Id
            });

            return ApiResponse<IEnumerable<IssueBookDto>>.SuccessResponse(bookDtos, "Books retrieved successfully.");
        }

        public async Task<bool> ReturnBookAsync(string issueBookId)
        {
            var issueRecord = await context.IssueBooks
                .Include(i => i.Book)
                .FirstOrDefaultAsync(i => i.Id == issueBookId);

            if (issueRecord == null)
                return false;


            issueRecord.Book.AvailableCopies += 1;


            context.IssueBooks.Remove(issueRecord);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<ApiResponse<bool>> UpdateBookAsync(BookDto dto)
        {
            Book? books = await context.Books.FindAsync(dto.Id);
            if (books == null)
                return ApiResponse<bool>.Fail("Member not found.");

            books.Author = dto.Author;
            books.Title = dto.Title;
            books.Category = dto.Category;
            books.ISBN = dto.ISBN;
            if (dto.AvailableCopies != null)
            {
                books.AvailableCopies = dto.AvailableCopies.Value;
            }

            if (dto.TotalCopies != null)
            {
                books.TotalCopies = dto.TotalCopies.Value;
            }


            await context.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Book updated successfully.");
        }

        public async Task<ApiResponse<string>> AddBookAsync(BookDto dto)
        {
            Book book = new()
            {
                Title = dto.Title,
                Author = dto.Author,
                ISBN = dto.ISBN,
                CreatedBy = "Admin",
                Category = dto.Category ?? "ALL",
                TotalCopies = dto.TotalCopies ?? 0,
                AvailableCopies = dto.TotalCopies ?? 0
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse(book.Title, "Book added successfully.", 201);
        }

        public async Task<ApiResponse<bool>> DeleteBookAsync(string id)
        {
            Book? book = await context.Books.FirstOrDefaultAsync(a => a.Id == id);

            if (book == null)
            {
                return ApiResponse<bool>.Fail("Book not found.", 404);
            }

            context.Books.Remove(book);
            await context.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Book deleted successfully.");
        }

        public Task<ApiResponse<IEnumerable<BookDto>>> SearchBooksAsync(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<string>> AssignBookAsync(IssueBookDto dto)
        {
            Book? book = await context.Books.FindAsync(dto.BookId);
            if (book is null)
                return ApiResponse<string>.Fail(" Book not found.");

            Member? member = await context.Members.FindAsync(dto.MemberId);
            if (member is null)
                return ApiResponse<string>.Fail("Member not found.");

            if (book.AvailableCopies <= 0)
                return ApiResponse<string>.Fail("No available copies for this book.");

            bool alreadyIssued = await context.IssueBooks
                .AnyAsync(i =>
                    i.BookId == dto.BookId && i.MemberId == dto.MemberId &&
                    false); // <- Replace false with your actual condition

            if (alreadyIssued)
                return ApiResponse<string>.Fail("This book is already issued to the selected member.");

            Database.Entities.IssueBook issue = new()
            {
                BookId = dto.BookId, MemberId = dto.MemberId, IssueDate = dto.IssueDate, DueDate = dto.DueDate
            };

            book.AvailableCopies--;

            context.IssueBooks.Add(issue);
            context.Books.Update(book);

            await context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("Book assigned successfully.");
        }
    }
}