using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Interfaces.Base
{
    public interface IBase<TEntity> where TEntity : class
    {
        TEntity GetById(Guid id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllActive();
        IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression);
        int Total(Expression<Func<TEntity, bool>> expression);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void AddOrUpdate(TEntity entity);
        void Remove(TEntity entity);
        void Dispose();
    }
}
