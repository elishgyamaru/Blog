using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Blog.Api.Models
{
    public class ApplicationUser:IdentityUser<int>,IBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public ICollection<BlogEntity> Blogs { get; set; }
    }
}