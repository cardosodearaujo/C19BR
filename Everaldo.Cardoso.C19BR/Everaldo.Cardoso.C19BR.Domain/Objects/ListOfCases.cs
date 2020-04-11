using System.Collections.Generic;

namespace Everaldo.Cardoso.C19BR.Domain.Objects
{
    public class ListOfCases : Header
    {
        public IList<Case> results { get; set; }
    }
}
