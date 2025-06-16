using Library.Common.ApiResponse;
using Library.Common.DTO;
using Library.Services.IssueBook;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookIssueController(IBookIssueService bookIssueService) : Controller
    {
        [HttpPost("assign")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignBook(IssueBookDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.BookId) || string.IsNullOrWhiteSpace(dto.MemberId))
            {
                TempData["Error"] = "Invalid book or member selection.";
                return RedirectToAction("Index", "Home");
            }

            ApiResponse<string> response = await bookIssueService.AssignBookAsync(dto);

            if (response.Success)
            {
                TempData["Success"] = response.Message;
            }
            else
            {
                TempData["Error"] = response.Message;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}