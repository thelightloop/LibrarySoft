using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Database.SeedData
{
    public static class IdentitySeedData
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Define roles to seed
            string[] roles = new[] { "SuperAdmin", "Admin", "NormalUser" };

            // Ensure all roles exist
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed users with their roles
            await CreateUserWithRoleAsync(userManager, "superadmin", "superadmin@gmail.com", "SuperAdminPassword123!", "SuperAdmin");
            await CreateUserWithRoleAsync(userManager, "admin", "admin@gmail.com", "AdminPassword123!", "Admin");
            await CreateUserWithRoleAsync(userManager, "normaluser", "user@gmail.com", "UserPassword123!", "NormalUser");
        }

        private static async Task CreateUserWithRoleAsync(
            UserManager<IdentityUser> userManager,
            string username,
            string email,
            string password,
            string role)
        {
            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser != null) return;

            var newUser = new IdentityUser
            {
                UserName = username,
                Email = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, role);
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user '{username}': {errors}");
            }
        }
    }
}
