using System.Collections.Generic;

namespace Everaldo.Cardoso.C19BR.Domain.Objects.Brasil
{
    public class Header
    {
        public Header()
        {
            count = 0;
            next = null;
            previous = null;
        }
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        
    }
}
