using Domain.Interfaces.Base;

namespace Application.Interface.Application
{
    public interface IApplicationBase<TEntity> : IBase<TEntity> where TEntity : class
    {
        void Commit();
        void Rollback();
    }
}
