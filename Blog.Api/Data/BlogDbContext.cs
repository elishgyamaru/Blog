using Blog.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Data
{
    public class BlogDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> dbCtxOptions):base(dbCtxOptions)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BlogEntity>()
            .HasOne(user=>user.Author)
            .WithMany(b=>b.Blogs)
            .HasForeignKey(a=>a.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<BlogTag>().HasKey(key=>new {key.BlogId,key.TagId});
            builder.Entity<BlogTag>()
            .HasOne(bt=>bt.Blog)
            .WithMany(bt=>bt.Tags)
            .HasForeignKey(key=>key.BlogId);

            builder.Entity<BlogTag>()
            .HasOne(t=>t.Tag)
            .WithMany(b=>b.Blogs)
            .HasForeignKey(bt=>bt.TagId);
        }
    }
}