using System.Threading.Tasks;
using Blog.Api.Data;
using Blog.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IBlogRepository<ApplicationUser> _repo;

        public UserController(
            IBlogRepository<ApplicationUser> repo
        )
        {
            this._repo = repo;
        }
        //api/User/1
        [HttpGet("{id}",Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user= await _repo.GetUser(id);
            return Ok(user);
        }

        //api/User/
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users= await _repo.GetUsers();
            return Ok(users);
        }
        
    }
}