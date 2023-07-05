using Microsoft.AspNetCore.Identity;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Roles;

namespace ReactRoastDotnet.Data.Configurations;

// ONLY USED IN DEVELOPMENT
public static class UserConfiguration
{
    public static async Task Initialize(AppDbContext appDbContext, UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var demoUser = new User
            {
                FirstName = "Demo",
                LastName = "User",
                UserName = "demo@gmail.com",
                Email = "demo@gmail.com",
                DateCreated = DateTime.UtcNow,
            };
            await userManager.CreateAsync(demoUser, "P@ssw0rd");
            await userManager.AddToRoleAsync(demoUser, DemoUserRole.Name);

            var user = new User
            {
                FirstName = "User",
                LastName = "Test",
                UserName = "test@gmail.com",
                Email = "test@gmail.com",
                DateCreated = DateTime.UtcNow,
            };
            await userManager.CreateAsync(user, "P@ssw0rd");
            await userManager.AddToRoleAsync(user, UserRole.Name);

            var admin = new User
            {
                FirstName = "Admin",
                LastName = "Test",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                DateCreated = DateTime.UtcNow,
            };

            await userManager.CreateAsync(admin, "P@ssw0rd");
            await userManager.AddToRolesAsync(admin, new[] { AdministratorRole.Name, UserRole.Name });
        }
    }
}