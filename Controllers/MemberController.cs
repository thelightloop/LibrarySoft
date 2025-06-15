using Library.Common.DTO;
using Library.Services.MemberManagement;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("[controller]")]
    public class MemberController(IMemberService memberService) : Controller
    {
        [HttpPost("AddMember")]
        public async Task<IActionResult> AddMember(MemberDto dto)
        {
            if (ModelState.IsValid)
            {
                await memberService.AddMemberAsync(dto);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteMember(string id)
        {
            await memberService.DeleteAsync(id);
            return RedirectToAction("Index", "Home");
        }
    }
}