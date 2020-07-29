using System;
using System.Collections.Generic;
using Blog.Api.Models;

namespace Blog.Api.Dtos
{
    public class BlogToReturnDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime LastEdited { get; set; }
        public bool isDraft { get; set; }
        public string AuthorId { get; set; }
        public UserToReturnDto Author { get; set; }
        public ICollection<BlogTag> Tags { get; set; }
    }
}