﻿using System.Collections.Generic;

namespace Everaldo.Cardoso.C19BR.Domain.Objects.Brasil
{
    public class ListOfReportCard : Header
    {
        public ListOfReportCard() : base()
        {
            results = new List<ReportCard>();
        }
        public IList<ReportCard> results { get; set; }
    }
}
