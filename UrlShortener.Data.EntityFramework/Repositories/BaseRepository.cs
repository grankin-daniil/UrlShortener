using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UrlShortener.Data.EntityFramework.Entities;

namespace UrlShortener.Data.EntityFramework.Repositories
{
    public class BaseRepository<T, TId> 
        where T : class, IEntityBase<TId>, new() 
        where TId : struct
    {
        private readonly UrlShortenerDbContext _context;

        public BaseRepository(UrlShortenerDbContext context)
        {
            _context = context;
        }
        
        public T Get(TId id)
        {
            return _context.Set<T>().Find(id);
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }
    }
}
