using Domain.Interfaces.Base;
using Domain.Interfaces.UnitOfWork;

namespace Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> : IUnitOfWork, IBase<TEntity> where TEntity : class
    {
    }
}
