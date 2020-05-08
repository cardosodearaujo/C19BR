using System.Collections.Generic;

namespace Everaldo.Cardoso.C19BR.Domain.Objects.Brasil
{
    public class ListOfCasesCity : Header
    {
        public ListOfCasesCity() : base()
        {
            results = new List<CityCases>();
        }
        public IList<CityCases> results { get; set; }
    }
}
