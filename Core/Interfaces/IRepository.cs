
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;
using System.Linq;

namespace Core.Interfaces
{

    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(object id);
        IQueryable<T> List();
        IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        T FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
