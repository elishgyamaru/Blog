namespace Blog.Api.Dtos
{
    public class BlogCreationDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsDraft { get; set; }
    }
}