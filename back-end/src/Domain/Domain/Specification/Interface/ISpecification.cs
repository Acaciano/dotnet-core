
namespace Domain.Specification.Interface
{
    public interface ISpecification<in T>
    {
        bool IsSatisfiedBy(T entity);
    }
}
