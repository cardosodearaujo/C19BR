using System.Collections.Generic;

namespace Everaldo.Cardoso.C19BR.Domain.Objects.World
{
    public class Header
    {
        public Header()
        {
            data = new Case();
        }

        public Case data { get; set; }
        public bool _cacheHit { get; set; }
    }
}
