using Everaldo.Cardoso.C19BR.Framework.Interfaces;
using System;

namespace Everaldo.Cardoso.C19BR.Framework.Specifications
{
    public class DateValidSpecification : ISpecification<DateTime>
    {
        public bool IsSatisfiedBy(DateTime objeto)
        {
            if (DateTime.Parse(objeto.ToString()) == null)
            {
                return false;
            }
            else if (DateTime.Parse(objeto.ToString()) == DateTime.MinValue)
            {
                return false;   
            }
            else if ((DateTime.Parse(objeto.ToString()) == DateTime.MaxValue))
            {
                return false;
            }
            return true;
        }
    }
}
