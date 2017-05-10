using Domain.Validation.Base;
using Domain.Validation.Interface;

namespace Domain.Validation
{
    public class Validation<TEntity> : FiscalBase<TEntity> where TEntity : class
    {
    }
}
