using System;

namespace Everaldo.Cardoso.C19BR.Domain.Objects
{
    public class Case
    {
        public string city { get; set; }
        public string city_ibge_code { get; set; }
        public long confirmed { get; set; }
        public long confirmed_per_100k_inhabitants { get; set; }
        public DateTime date { get; set; }
        public decimal? death_rate { get; set; }
        public long deaths { get; set; }
        public long estimated_population_2019 { get; set; }
        public bool is_last { get; set; }
        public string place_type { get; set; }
        public string state { get; set; }
    }
}
