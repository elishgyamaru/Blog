using Microsoft.AspNetCore.Identity;

namespace Blog.Api.Models
{
    public class ApplicationRole:IdentityRole<int>,IBaseEntity
    {
        
    }
}