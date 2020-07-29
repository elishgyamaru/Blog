using Microsoft.Extensions.DependencyInjection;

namespace Blog.Api._Extensions
{
    public static class AuthorizationPolicies
    {
        public const string CanCreateBlogAndUsers="CanCreateBlogAndUsers";
        public const string CanCreateBlog="CanCreateBlog";
        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(option=>{
                option.AddPolicy(CanCreateBlog,policy=>policy.RequireRole("Admin"));
                option.AddPolicy(CanCreateBlogAndUsers,policy=>
                policy.RequireRole("Blogger")
                .RequireRole("Admin")
                );
            });
        }
    }
}