using System.Collections.Generic;
using System.Linq;
using Blog.Api.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Blog.Api.Data
{
    public static class Seed
    {
        public static void SeedData(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager
        )
        {
            if (!userManager.Users.Any())
            {
                var users = System.IO.File.ReadAllText("Data/User.json");
                var mockUsers = JsonConvert.DeserializeObject<IEnumerable<ApplicationUser>>(users);
                var roles = new List<ApplicationRole>()
                {
                    new ApplicationRole(){Name="Admin"},
                    new ApplicationRole(){Name="Blogger"}
                };
                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }
                foreach (var user in mockUsers)
                {
                    userManager.CreateAsync(user, "Blogger@1").Wait();
                    userManager.AddToRoleAsync(user,"Blogger").Wait();
                }
                var admin = new ApplicationUser()
                {
                    UserName = "admin"
                };
                if (userManager.CreateAsync(admin, "Admin@1").Result.Succeeded)
                {
                    var adminUser = userManager.FindByNameAsync("admin").Result;
                    userManager.AddToRolesAsync(adminUser, new[] { "Admin", "Blogger" });
                }
            }

        }
    }
}