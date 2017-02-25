namespace Template.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(IDbContextResolver contextResolver)
        {
            _context = contextResolver.Resolve<T>();
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> All()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<T> All(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _dbSet.AsNoTracking();
            return GetIncluding(query, includeProperties).ToList();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> results = _dbSet.Where(predicate).ToList();
            return results;
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate,
          params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _dbSet.Where(predicate);
            var queryInclude = GetIncluding(query, includeProperties);
            IEnumerable<T> results = queryInclude.ToList();
            return results;
        }

        public T FindByKey(long id)
        {
            Expression<Func<T, bool>> lambda = Utilities.BuildLambdaForFindByKey<T>(id);
            return _dbSet.SingleOrDefault(lambda);
        }

        public T FindByKey(long id, params Expression<Func<T, object>>[] includeProperties)
        {
            Expression<Func<T, bool>> lambda = Utilities.BuildLambdaForFindByKey<T>(id);
            var entity = GetIncluding(_dbSet, includeProperties);

            return entity.FirstOrDefault(lambda);
        }

        public T Insert(T entity)
        {
            var t = _dbSet.Attach(entity);
            _context.Entry(t).State = EntityState.Added;

            return t;
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            var entity = FindByKey(id);
            if (entity == null) return;

            _dbSet.Remove(entity);
        }
        
        private IQueryable<T> GetIncluding(IQueryable<T> queryable, 
            params Expression<Func<T, object>>[] includeProperties)
        {
            return includeProperties.Aggregate
              (queryable, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}