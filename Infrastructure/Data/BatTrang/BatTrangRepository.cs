
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Linq.Expressions;

namespace Infrastructure.Data.BatTrang
{
    public class BatTrangRepository<T> : IBatTrangRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;

        public BatTrangRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T GetById(object id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual IQueryable<T> List()
        {
            return _dbContext.Set<T>();
        }

        public virtual IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        public virtual T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            var x = _dbContext.Set<T>();

            return x.FirstOrDefault(predicate);
        }
    }
}
