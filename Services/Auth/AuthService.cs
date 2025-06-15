using Library.Common.ApiResponse;
using Library.Common.DTO;
using Microsoft.AspNetCore.Identity;

namespace Library.Services.Auth
{
    public class AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : IAuthService
    {
        public async Task<ApiResponse<string>> RegisterAsync(RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
            {
                return new ApiResponse<string>(false, "Passwords do not match", null, 400);
            }

            var user = new IdentityUser
            {
                UserName = dto.Username,
                Email = dto.Email
            };

            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return new ApiResponse<string>(false, $"Registration failed: {errors}", null, 400);
            }

            // Optionally assign a default role:
            await userManager.AddToRoleAsync(user, "NormalUser");

            return new ApiResponse<string>(true, "User registered successfully", null, 201);
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginDto dto)
        {
            var result = await signInManager.PasswordSignInAsync(dto.Username, dto.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return new ApiResponse<string>(false, "Invalid username or password", null, 401);
            }

            return new ApiResponse<string>(true, "Login successful", null, 200);
        }
    }
}
