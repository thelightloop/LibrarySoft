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
            if (dto.MembershipStartDate == null)
                return ApiResponse<string>.Fail("Membership start date is required.");

            var member = new Member
            {
                Id = dto.Id,
                FullName = dto.FullName,
                Email = dto.Email,
                ContactNumber = dto.ContactNumber,
                MembershipType = dto.MembershipType,
                MembershipStartDate = dto.MembershipStartDate.Value.Date,
                CreatedBy = "Admin"
            };

            await context.Members.AddAsync(member);
            await context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse(dto.Id, " Member added successfully.");
        }

        public async Task<ApiResponse<bool>> UpdateMemberAsync(string id, MemberDto dto)
        {
            var member = await context.Members.FindAsync(id);
            if (member is null)
                return ApiResponse<bool>.Fail(" Member not found.");

            member.FullName = dto.FullName;
            member.Email = dto.Email;
            member.ContactNumber = dto.ContactNumber;

            await context.SaveChangesAsync();
            return ApiResponse<bool>.SuccessResponse(true, " Member updated successfully.");
        }

        public async Task<ApiResponse<IEnumerable<MemberDto>>> GetAllMembersAsync()
        {
            var members = await context.Members
                .Select(m => new MemberDto
                {
                    Id = m.Id,
                    FullName = m.FullName,
                    Email = m.Email,
                    ContactNumber = m.ContactNumber,
                    MembershipType = m.MembershipType,
                    MembershipStartDate = m.MembershipStartDate
                }).ToListAsync();

            return ApiResponse<IEnumerable<MemberDto>>.SuccessResponse(members, " Members fetched successfully.");
        }

        public async Task<ApiResponse<MemberDto>> GetByIdAsync(string id)
        {
            var member = await context.Members.FindAsync(id);
            if (member is null)
                return ApiResponse<MemberDto>.Fail(" Member not found.");

            var dto = mapper.Map<MemberDto>(member);
            return ApiResponse<MemberDto>.SuccessResponse(dto, " Member fetched successfully.");
        }

        public async Task<ApiResponse<bool>> DeleteAsync(string id)
        {
            var member = await context.Members.FindAsync(id);
            if (member is null)
                return ApiResponse<bool>.Fail(" Member not found.");

            context.Members.Remove(member);
            await context.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, " Member deleted successfully.");
        }

        public async Task<ApiResponse<bool>> UpdateMemberAsync(MemberDto dto)
        {
            var member = await context.Members.FindAsync(dto.Id);
            if (member == null)
                return ApiResponse<bool>.Fail("Member not found.");

            member.FullName = dto.FullName;
            member.Email = dto.Email;
            member.ContactNumber = dto.ContactNumber;
            member.MembershipType = dto.MembershipType;
            if (dto.MembershipStartDate != null)
            {
                member.MembershipStartDate = dto.MembershipStartDate.Value;
            }

            await context.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, " Member updated successfully.");
        }
    }
}