namespace Everaldo.Cardoso.C19BR.Domain.Objects.World
{
    public class LatestData
    {
        public LatestData()
        {
            calculated = new Calculated();
        }

        public long? deaths { get; set; }
        public long? confirmed { get; set; }
        public long? recovered { get; set; }
        public long? critical { get; set; }
        public Calculated calculated { get; set; }
    }
}
