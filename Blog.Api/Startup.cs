using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Blog.Api.Helpers;
using Blog.Api._Extensions;
using Blog.Api.Models;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace Blog.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this._env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.Configure<JwtConfigurations>(Configuration.GetSection("Jwt"));
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<BlogDbContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("DefaultDb")));
            services.AddControllers();
            services.AddTransient(typeof(IBlogRepository<>), typeof(BlogRepository<>));
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<BlogDbContext>();
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtConfigurations>();
            services.AddAuthenticationServices(jwtSettings);
            services.AddAuthorizationPolicies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                        Path.Combine(env.ContentRootPath, "BlogImages")),
                RequestPath = "/BlogImages"
            });
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
