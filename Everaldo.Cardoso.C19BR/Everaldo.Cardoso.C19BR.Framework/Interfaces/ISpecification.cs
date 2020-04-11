namespace Everaldo.Cardoso.C19BR.Framework.Interfaces
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T objeto);
    }
}
