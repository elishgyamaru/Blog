using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Api.Data;
using Blog.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Blog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host=CreateHostBuilder(args).Build();
            using(var scope=host.Services.CreateScope())
            {
                var context=scope.ServiceProvider.GetService<BlogDbContext>();
                var roleManager=scope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();
                var userManager=scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                context.Database.Migrate();
                Seed.SeedData(userManager,roleManager);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
