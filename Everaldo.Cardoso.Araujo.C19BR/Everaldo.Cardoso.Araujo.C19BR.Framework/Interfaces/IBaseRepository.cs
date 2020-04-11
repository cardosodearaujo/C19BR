using NHibernate;
using Everaldo.Cardoso.Araujo.C19BR.Framework.Models;
using System.Collections.Generic;

namespace Everaldo.Cardoso.Araujo.C19BR.Framework.Interfaces
{
    public interface IBaseRepository<obj>
    {
        ISession Session { get; set; }
        ITransaction Transaction { get; set; }
        void BeginSession();
        void BeginTransaction();
        void Commit();
        void Rollback();
        void Flush();
        void Clear();
        obj Save(obj entity);
        IEnumerable<obj> Save(IEnumerable<obj> list);                
        bool Delete(obj entity);
        bool DeleteAllByProperties(IEnumerable<CriteriaConditions> conditions);
        bool DeleteAll();
        obj SelectById(object id);
        IEnumerable<obj> SelectAll();
        obj SelectByProperty(string property, object value);
        IEnumerable<obj> SelectAllByProperty(string property, object value);
        IEnumerable<obj> SelectAllByProperties(IEnumerable<CriteriaConditions> conditions);
    }
}
