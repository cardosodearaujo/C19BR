namespace Everaldo.Cardoso.C19BR.Domain.Objects.World
{
    public class Calculated
    {
        public decimal? death_rate { get; set; }
        public decimal? recovery_rate { get; set; }
        public decimal? recovered_vs_death_ratio { get; set; }
        public long? cases_per_million_population { get; set; }
    }
}
