using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Data
{
    public class BlogRepository<T> : IBlogRepository<T> where T:class,IBaseEntity
    {
        private readonly BlogDbContext _context;
        public BlogRepository(BlogDbContext context)
        {
            this._context = context;
        }

        public void Add(T t)
        {
            _context.Set<T>().Add(t);
        }

        public void Delete(T t)
        {
            _context.Set<T>().Remove(t);
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeList)
        {
            IQueryable<T> query=_context.Set<T>();
            foreach(var include in includeList)
            {
                query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetSingleAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(entity=>entity.Id==id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetSingleIncluding(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includeList)
        {
            IQueryable<T> query=_context.Set<T>();
            foreach(var include in includeList)
            {
                query.Include(include);
            }
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<ApplicationUser> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u=>u.Id==id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}