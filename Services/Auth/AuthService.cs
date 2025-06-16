using Library.Common.ApiResponse;
using Library.Common.DTO;
using Microsoft.AspNetCore.Identity;

namespace Library.Services.Auth
{
    public class AuthService
        (UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : IAuthService
    {
        public async Task<ApiResponse<string>> RegisterAsync(RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
            {
                return ApiResponse<string>.SuccessResponse("Passwords do not match");
            }

            var user = new IdentityUser { UserName = dto.Username, Email = dto.Email };

            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return ApiResponse<string>.Fail($"Registration failed: {errors}");
            }

            // Optionally assign a default role:
            await userManager.AddToRoleAsync(user, "NormalUser");

            return ApiResponse<string>.SuccessResponse("User registered successfully");
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginDto dto)
        {
            var result = await signInManager.PasswordSignInAsync(dto.Username, dto.Password, isPersistent: false,
                lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return ApiResponse<string>.Fail("Invalid username or password");
            }

            return ApiResponse<string>.SuccessResponse("Login successful");
        }
    }
}