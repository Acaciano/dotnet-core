using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Validation.Interface
{
    public interface ISelfValidation<out TEntity>
    {
        [NotMapped]
        ValidationResult ResultValidation { get;}
        bool IsValid(IValidation<TEntity> validation);
    }
}
