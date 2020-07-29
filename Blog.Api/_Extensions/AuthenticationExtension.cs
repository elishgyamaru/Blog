using System.Text;
using Blog.Api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Api._Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddAuthenticationServices(
            this IServiceCollection services,
            JwtConfigurations jwtSettings
        )
        {
            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = jwtSettings.Issuer,
                    //ValidAudience = jwtSettings.Audience,
                    ValidateAudience=false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });
            return services;
        }
        // public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        // {
        //     app.UseAuthentication();
        //     app.UseAuthorization();
        //     return app;
        // }
    }
}