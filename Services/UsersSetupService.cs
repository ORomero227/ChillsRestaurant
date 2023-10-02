using Microsoft.AspNetCore.Identity;
using static System.Formats.Asn1.AsnWriter;
using System.Data;
using ChillsRestaurant.Models;

namespace ChillsRestaurant.Services
{
    public class UsersSetupService
    {
        private readonly IServiceProvider _serviceProvider;

        public UsersSetupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SetupRolesAndUsersAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Manager", "Employee", "Client" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string email = "manager@chillsrestaurant.com";
                string password = "Rataalada31!";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = "manager1",
                        Email = email,
                        Name = "Manager",
                        PhoneNumber = "787-000-0000",
                        SecondaryPhoneNumber = "Unregistered",
                        Address = "Unregistered",
                        Photo = "avatar-default.png",
                        Role = "Manager",
                        AccountStatus = "enable",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        TwoFactorEnabled = false
                    };

                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Manager");
                }

            }
        }
    }
}
