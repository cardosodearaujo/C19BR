namespace Everaldo.Cardoso.C19BR.Domain.ValueObjects
{
    public class ItemSearchListVO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Confirmed { get; set; }
        public string Recovered { get; set; }
        public string Deaths { get; set; }
        public string DeathRate { get; set; }
        public string DeathsToday { get; set; }
        public string ConfirmedToday { get; set; }
        public string IBGE { get; set; }
    }
}
