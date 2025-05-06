using CleanArchitecture.SampleProject.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.SampleProject.Identity.Seed
{
    public static class UserCreator
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var applicationUser = new ApplicationUser
            {
                FirstName = "Deepak",
                LastName = "Mittal",
                UserName = "dmittal",
                Email = "deepak@test.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(applicationUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(applicationUser, "Password@2024");
            }
        }
    }
}