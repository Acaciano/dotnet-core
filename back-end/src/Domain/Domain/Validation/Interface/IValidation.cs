
namespace Domain.Validation.Interface
{
    public interface IValidation<in TEntity>
    {
        ValidationResult Validate(TEntity entity);
    }
}
