using Everaldo.Cardoso.C19BR.Framework.NHibernate;
using NHibernate;
using NHibernate.Cfg;

namespace Everaldo.Cardoso.C19BR.API.NHibernate
{
    public class NHibernateHelper 
    {
        private static ISessionFactory sessionFactory;

        public NHibernateHelper(){}

        public static ISession GetSession()
        {
            if (sessionFactory == null)
            {
                lock (typeof(NHibernateHelper))
                {
                    if (sessionFactory == null)
                    {
                        var cfg =
                            new Configuration()
                            .SetProperty("dialect", NHibernateConfigurationData.Dialect)
                            .SetProperty("connection.connection_string", NHibernateConfigurationData.Connection.ToString())
                            .SetProperty("connection.isolation", "ReadCommitted")
                            .AddAssembly(NHibernateConfigurationData.Assembly);
                        sessionFactory = cfg.BuildSessionFactory();
                    }
                }
            }
            return sessionFactory.OpenSession();
        }
    }
}