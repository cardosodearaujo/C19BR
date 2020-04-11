using Everaldo.Cardoso.C19BR.Framework.Interfaces;

namespace Everaldo.Cardoso.C19BR.Framework.Specifications
{
    public class ObjectNullSpecification : ISpecification<object>
    {
        public bool IsSatisfiedBy(object objeto)
        {
            return (objeto == null ? true : false);            
        }
    }
}
