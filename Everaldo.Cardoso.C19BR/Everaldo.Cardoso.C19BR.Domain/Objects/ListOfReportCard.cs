using System.Collections.Generic;

namespace Everaldo.Cardoso.C19BR.Domain.Objects
{
    public class ListOfReportCard : Header
    {
        public IList<ReportCard> results { get; set; }
    }
}
