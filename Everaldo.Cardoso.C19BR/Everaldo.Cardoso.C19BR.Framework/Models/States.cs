namespace Everaldo.Cardoso.C19BR.Framework.Models
{
    public class States
    {
        public int Code { get; set; }
        public string UF { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name + " - " + UF; 
        }
    }
}
