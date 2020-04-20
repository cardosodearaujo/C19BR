using System;

namespace Everaldo.Cardoso.C19BR.Domain.Objects.World
{
    public class Day
    {
        public DateTime updated_at { get; set; }
        public DateTime date { get; set; }
        public long? deaths { get; set; }
        public long? confirmed { get; set; }
        public long? active { get; set; }
        public long? recovered { get; set; }
        public long? new_confirmed { get; set; }
        public long? new_recovered { get; set; }
        public long? new_deaths { get; set; }
        public bool is_in_progress { get; set; }
    }
}
