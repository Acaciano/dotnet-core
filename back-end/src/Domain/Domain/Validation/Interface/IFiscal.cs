namespace Domain.Validation.Interface
{
    public interface IFiscal<in TEntity>
    {
        ValidationResult Validate(TEntity entity);
    }
}