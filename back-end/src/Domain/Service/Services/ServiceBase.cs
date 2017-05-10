using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Interfaces.UnitOfWork;
using Domain.Validation;
using Microsoft.Practices.ServiceLocation;

namespace Domain.Service.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repositoryBase;

        public ServiceBase(IRepositoryBase<TEntity> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public TEntity GetById(Guid id)
        {
            return _repositoryBase.GetById(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repositoryBase.GetAll();
        }

        public IEnumerable<TEntity> GetAllActive()
        {
            return _repositoryBase.GetAllActive();
        }

        public IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression)
        {
            return _repositoryBase.GetBy(expression);
        }

        public int Total(Expression<Func<TEntity, bool>> expression)
        {
            return _repositoryBase.Total(expression);
        }

        public void Add(TEntity entity)
        {
            _repositoryBase.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _repositoryBase.Update(entity);
        }

        public void AddOrUpdate(TEntity entity)
        {
            _repositoryBase.AddOrUpdate(entity);
        }

        public void Remove(TEntity entity)
        {
            _repositoryBase.Remove(entity);
        }

        public void Commit()
        {
            _repositoryBase?.Commit();
        }

        public void Rollback()
        {
            _repositoryBase?.Rollback();
        }

        public void Dispose()
        {
            _repositoryBase.Dispose();
        }
    }
}
