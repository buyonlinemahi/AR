using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Core.Base.Data
{
    public interface IBaseRepository<T>
        where T : class
    {
        IDbSet<T> GetDbSet();

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Expression<Func<T, bool>> where);

        IEnumerable<T> GetAll(int maxRecordCount);

        T GetById(int id);

        T GetById(Guid id);

        T Add(T item);

        int Update(T entity);

        int Update(T item, params Expression<Func<T, object>>[] properties);

        void Delete(T item);

        void Delete(Expression<Func<T, bool>> where);
    }
}