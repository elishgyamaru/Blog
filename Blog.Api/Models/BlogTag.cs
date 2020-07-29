namespace Blog.Api.Models
{
    public class BlogTag
    {
        public int BlogId { get; set; }
        public BlogEntity Blog { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}