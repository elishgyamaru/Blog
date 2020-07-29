using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Blog.Api.Models
{
    public class BlogEntity : IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PreviewContent { get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastEdited { get; set; } = DateTime.Now;
        public bool isDraft { get; set; }

        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public ICollection<BlogTag> Tags { get; set; }
    }
}