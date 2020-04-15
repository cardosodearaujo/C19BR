using System.Collections.Generic;

namespace Everaldo.Cardoso.C19BR.Domain.Objects.World
{
    public class ListOfCases
    {
        public ListOfCases()
        {
            data = new List<Case>();
        }

        public IList<Case> data { get; set; }
        public bool _cacheHit { get; set; }
    }
}
