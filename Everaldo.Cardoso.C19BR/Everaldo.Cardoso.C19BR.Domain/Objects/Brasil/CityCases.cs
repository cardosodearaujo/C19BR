using System;

namespace Everaldo.Cardoso.C19BR.Domain.Objects.Brasil
{
    public class CityCases
    {
        public string city { get; set; }
        public long? city_ibge_code { get; set; }
        public DateTime date { get; set; }
        public long? epidemiological_week { get; set; }
        public long? estimated_population_2019 { get; set; }
        public bool is_last { get; set; }
        public bool is_repeated { get; set; }        
        public long? last_available_confirmed { get; set; }
        public long? last_available_confirmed_per_100k_inhabitants { get; set; }
        public DateTime last_available_date { get; set; }
        public long? last_available_death_rate { get; set; }
        public long? last_available_deaths { get; set; }
        public long? new_confirmed { get; set; }
        public long? new_deaths { get; set; }
        public long? order_for_place { get; set; }
        public string place_type { get; set; }
        public string state { get; set; }
    }
}
