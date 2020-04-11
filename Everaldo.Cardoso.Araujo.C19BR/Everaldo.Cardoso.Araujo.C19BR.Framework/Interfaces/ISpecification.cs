namespace Everaldo.Cardoso.Araujo.C19BR.Framework.Interfaces
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T objeto);
    }
}
