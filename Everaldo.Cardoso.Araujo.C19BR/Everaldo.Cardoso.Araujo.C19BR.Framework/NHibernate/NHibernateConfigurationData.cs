using Everaldo.Cardoso.Araujo.C19BR.Framework.Models;

namespace Everaldo.Cardoso.Araujo.C19BR.Framework.NHibernate
{
    public static class NHibernateConfigurationData
    {
        public static string Dialect { get; set; }
        public static ConnectionData Connection { get; set; }
        public static string Assembly { get; set; }
    }
}
