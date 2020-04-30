using System;

namespace Everaldo.Cardoso.C19BR.Domain.ValueObjects
{
    public class CasesNotAccumulatedStateVO
    {
        public DateTime Date { get; set; }
        public decimal Confirmed { get; set; }
        public decimal Deaths { get; set; }
    }
}
