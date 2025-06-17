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

        [HttpPost("return")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnBook(string AssignmentId)
        {
            if (string.IsNullOrEmpty(AssignmentId))
            {
                TempData["Error"] = "Invalid request.";
                return RedirectToAction("Index", "Home");
            }

            var result = await bookIssueService.ReturnBookAsync(AssignmentId);

            if (!result)
                TempData["Error"] = "Issued book record not found.";
            else
                TempData["Success"] = "Book returned successfully.";

            return RedirectToAction("Index", "Home");
        }
    }
}