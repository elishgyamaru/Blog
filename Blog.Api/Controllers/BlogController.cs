using System;
using System.IO;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Api._Extensions;
using Blog.Api.Data;
using Blog.Api.Dtos;
using Blog.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository<BlogEntity> _repo;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<ApplicationUser> usermanager;

        public BlogController(
        IBlogRepository<BlogEntity> repo,
        IMapper mapper,
        IWebHostEnvironment env,
        UserManager<ApplicationUser> usermanager
        )
        {
            this._repo = repo;
            this.mapper = mapper;
            this._env = env;
            this.usermanager = usermanager;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _repo.GetAllAsync(be=>be.Tags,be=>be.Author);
            return Ok(blogs);
        }
        [AllowAnonymous]
        [HttpGet("{id}", Name="GetBlog")]
        public async Task<IActionResult> GetBlog(int id)
        {
            var blogs = await _repo.GetSingleAsync(id);
            return Ok(blogs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(BlogCreationDto blogDto)
        {
            
            blogDto.Content=SaveImage(blogDto.Content);
            var userId=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var blog = mapper.Map<BlogEntity>(blogDto);
            blog.PreviewContent=SetPreviewContent(blog.Content);
            blog.AuthorId=userId;
            blog.Author=
            _repo.Add(blog);
            if (await _repo.SaveAll())
            {
                var blogToReturn = mapper.Map<BlogToReturnDto>(blog);
                return CreatedAtRoute("GetBlog", new { id = blog.Id }, blogToReturn);
            }
            throw new Exception("Could not create blog");
        }

        private string SetPreviewContent(string content)
        {
            if(content.Length<500)
            {
                return content;
            }
                string subContent=content.Substring(0,500)+"...";
                int imageLocation=subContent.IndexOf("<img");
                if(imageLocation>-1 && imageLocation <200)
                {
                    string pattern="<img[^>]*>";
                    Regex imgRgx= new Regex(pattern);
                    subContent=imgRgx.Match(content).Value;
                }
                return subContent;
        }

        private string SaveImage(string content)
        {
            string myHostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";
            string path = "BlogImages"; 
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string baseImagePath=Path.Combine(_env.ContentRootPath,path);
            string pattern="<img[^>]*>";
            Regex imgRgx= new Regex(pattern);

            string extension=@"image\/\w*";
            Regex extRgx=new Regex(extension);
            
            foreach (Match img in imgRgx.Matches(content))
            {
                if(img.Value.IndexOf("base64,")!=-1)
                {
                string ext= extRgx.Match(img.Value).Value.Split('/')[1];
                string imageName = Guid.NewGuid() + "."+ext;
                string imgPath = Path.Combine(baseImagePath, imageName);
                string base64String=img.Value.Split(',')[1].Replace("\">","");
                byte[] imageBytes = Convert.FromBase64String(base64String);
                System.IO.File.WriteAllBytes(imgPath, imageBytes);
                string localImgPath="<img src=\""+Path.Combine(myHostUrl,path,imageName)+"\">";
                //content=imgRgx.Replace(content,localImgPath);
                content=content.Replace(img.Value,localImgPath);
                }
            }
            return content;

        }
        [HttpPut]
        public async Task<IActionResult> EditBlog(int id,BlogCreationDto editedblogDto)
        {
            var blog = mapper.Map<BlogEntity>(editedblogDto);
            //blog.Author=User.FindFirst
            var currentBlog= await _repo.GetSingleAsync(id);
            
            if (await _repo.SaveAll())
            {
                var blogToReturn = mapper.Map<BlogToReturnDto>(blog);
                return CreatedAtRoute("GetBlog", new { id = blog.Id }, blogToReturn);
            }
            throw new Exception("Could not create blog");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog=await _repo.GetSingleAsync(id);
            if(blog==null)
            {
                return BadRequest("Blog Not Found");
            }
            _repo.Delete(blog);
            if(await _repo.SaveAll())
            {
                return Ok("Blog deleted.");
            }
            return BadRequest("Error deleting blog");
        }
    }
}