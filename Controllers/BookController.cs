using Library.Common.DTO;
using Library.Services.IssueBook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class BookController(IBookIssueService bookService) : Controller
    {
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook(BookDto dto)
        {
            if (ModelState.IsValid)
            {
                await bookService.AddBookAsync(dto);
            }

            return RedirectToAction("Index", "Home");
        }
       
        

        [HttpPost("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            await bookService.DeleteBookAsync(id);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("UpdateBook")]
        public async Task<IActionResult> UpdateBook(BookDto dto)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            // Assuming you have an UpdateMemberAsync method in your service
            await bookService.UpdateBookAsync(dto);
            return RedirectToAction("Index", "Home");

            // If ModelState is invalid, you can handle it by reloading the page with errors
            //
        }
    }
}