using Library.Common.ApiResponse;
using Library.Common.DTO;
using Library.Services.IssueBook;
using Library.Services.MemberManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class HomeController(IBookIssueService bookIssueService, IMemberService memberService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            ApiResponse<IEnumerable<BookDto>> books = await bookIssueService.GetAllBooksAsync();

            ApiResponse<IEnumerable<MemberDto>> members = await memberService.GetAllMembersAsync();

            HomePageDto model = new() { BookDto = books.Data, MembersDto = members.Data };

            return View(model);
        }
    }
}