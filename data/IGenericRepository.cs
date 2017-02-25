namespace StorageManager.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> All();
        IEnumerable<T> All(params Expression<Func<T, object>>[] includeProperties);
        void Delete(long id);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T FindByKey(long id);
        T FindByKey(long id, params Expression<Func<T, object>>[] includeProperties);
        T Insert(T entity);
        void Update(T entity);
    }
}