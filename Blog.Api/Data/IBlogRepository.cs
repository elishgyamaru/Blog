using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Api.Models;

namespace Blog.Api.Data
{
    public interface IBlogRepository<T> where T:class,IBaseEntity
    {
         Task<ApplicationUser> GetUser(int id);
         Task<IEnumerable<ApplicationUser>> GetUsers();
         void Add(T t);
         void Delete(T t);
         Task<T> GetSingleAsync(int id);
         Task<T> GetSingleAsync(Expression<Func<T,bool>> predicate);
         Task<T> GetSingleIncluding(Expression<Func<T,bool>> predicate,Expression<Func<T,object>>[] includeList);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T,object>>[] includeList);
        Task<bool> SaveAll();
    }
}