using System.Collections.Generic;
namespace Blog.Api.Models
{
    public class Tag:IBaseEntity
    {
        public int Id {get;set;}
        
        public string Name {get;set;}
        
        public ICollection<BlogTag> Blogs { get; set; }
    }
}