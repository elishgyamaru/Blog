using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.Api._Extensions;
namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy=AuthorizationPolicies.CanCreateBlogAndUsers)]
    public class AdminController:ControllerBase
    {
        
    }
}