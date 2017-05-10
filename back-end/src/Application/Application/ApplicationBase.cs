using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Application.Interface.Application;
using Application.Validation;
using Domain.Interfaces.Services;
using Domain.Validation;

namespace Application.Application
{
    public class ApplicationBase<TEntity> : IApplicationBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceBase;

        public ApplicationBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public TEntity GetById(Guid id)
        {
            return _serviceBase.GetById(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _serviceBase.GetAll();
        }

        public IEnumerable<TEntity> GetAllActive()
        {
            return _serviceBase.GetAllActive();
        }

        public IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression)
        {
            return _serviceBase.GetBy(expression);
        }

        public int Total(Expression<Func<TEntity, bool>> expression)
        {
            return _serviceBase.Total(expression);
        }

        public void Add(TEntity entity)
        {
            _serviceBase.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _serviceBase.Update(entity);
        }

        public void AddOrUpdate(TEntity entity)
        {
            _serviceBase.AddOrUpdate(entity);
        }

        public void Remove(TEntity entity)
        {
            _serviceBase.Remove(entity);
        }

        public void Commit()
        {
            _serviceBase.Commit();
        }

        public void Rollback()
        {
            _serviceBase.Rollback();
        }

        public void Dispose()
        {
            _serviceBase.Dispose();
        }

        protected ValidationAppResult DomainToApplicationResult(ValidationResult result)
        {
            var validationAppResult = new ValidationAppResult();

            foreach (var validationError in result.Erros)
            {
                validationAppResult.Erros.Add(new ValidationAppError(validationError.Message));
            }
            validationAppResult.IsValid = result.IsValid;

            return validationAppResult;
        }
    }
}
