using AutoMapper;
using Library.Common.ApiResponse;
using Library.Common.DTO;
using Library.Database;
using Library.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.MemberManagement
{
    public class MemberService(AppDbContext context, IMapper mapper) : IMemberService
    {
        public async Task<ApiResponse<string>> AddMemberAsync(MemberDto dto)
        {
            if (dto.MembershipStartDate != null)
            {
                Member members = new()
                {
                    Id = dto.Id,
                    ContactNumber = dto.ContactNumber,
                    Email = dto.Email,
                    CreatedBy = "Admin",
                    MembershipType = dto.MembershipType ,
                    FullName = dto.FullName,
                    MembershipStartDate = dto.MembershipStartDate.Value.Date,
                };

                context.Members.Add(members);
                await context.SaveChangesAsync();
            }

            
            return new ApiResponse<string>(true, "Member added", dto.Id);
        }

        public async Task<ApiResponse<bool>> UpdateMemberAsync(string id, MemberDto dto)
        {
            Member? member = await context.Members.FindAsync(id);
            if (member == null) return new ApiResponse<bool>(false, "Member not found", false);

            member.FullName = dto.FullName;
            member.Email = dto.Email;
            member.ContactNumber = dto.ContactNumber;

            await context.SaveChangesAsync();
            return new ApiResponse<bool>(true, "Member updated", true);
        }

        public async Task<ApiResponse<IEnumerable<MemberDto>>> GetAllMembersAsync()
        {
            List<MemberDto> members = await context.Members
                .Select(m => new MemberDto
                {
                    Id = m.Id.ToString(),
                    FullName = m.FullName,
                    Email = m.Email,
                    ContactNumber = m.ContactNumber
                }).ToListAsync();

            return new ApiResponse<IEnumerable<MemberDto>>(true, "Fetched", members);
        }

        public async Task<ApiResponse<MemberDto>> GetByIdAsync(string id)
        {
            Member? member = await context.Members.FindAsync(id);
            if (member == null) return new ApiResponse<MemberDto>(false, "Not found", null);

            MemberDto? dto = mapper.Map<MemberDto>(member);
            return new ApiResponse<MemberDto>(true, "Fetched", dto);
        }

        public async Task<ApiResponse<bool>> DeleteAsync(string id)
        {
            Member? member = await context.Members.FindAsync(id);
            if (member == null) return new ApiResponse<bool>(false, "Not found", false);

            context.Members.Remove(member);
            await context.SaveChangesAsync();
            return new ApiResponse<bool>(true, "Deleted", true);
        }

    }


}
