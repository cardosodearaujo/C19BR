using Everaldo.Cardoso.C19BR.API.NHibernate;
using Everaldo.Cardoso.C19BR.Framework.Conversion;
using Everaldo.Cardoso.C19BR.Framework.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using System;
using Everaldo.Cardoso.C19BR.Framework.Models;

namespace Everaldo.Cardoso.C19BR.Framework.Bases
{
    public class BaseRepository<obj> : IBaseRepository<obj>
    {
        public BaseRepository()
        {
            BeginSession();
        }

        #region "Propriedades da sessão"
        public ISession Session { get; set; }

        public ITransaction Transaction { get; set; }
        #endregion

        #region "Controles de sessão"
        public void BeginSession()
        {
            Session = NHibernateHelper.GetSession();
        }

        public void BeginTransaction()
        {            
            Transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            if (Transaction != null && Transaction.IsActive) Transaction.Commit();
        }

        public void Rollback()
        {
            if (Transaction != null && Transaction.IsActive) Transaction.Rollback();
        }

        public void Flush()
        {
            if (Session != null && Session.IsOpen) Session.Flush();
        }

        public void Clear()
        {
            if (Session != null && Session.IsOpen) Session.Clear();
        }
        #endregion        

        #region "Persistência externa"
        public obj Save(obj entity)
        {
            try
            {
                BeginTransaction();
                SaveObject(entity);
                Commit();
                return entity;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
            finally
            {
                Flush();
            }            
        }

        public IEnumerable<obj> Save(IEnumerable<obj> list)
        {
            try
            {
                BeginTransaction();
                foreach (var entity in list)
                {
                    SaveObject(entity);
                }
                Commit();
                return list;
            }
            catch(Exception ex)
            {
                Rollback();
                throw ex;
            }
            finally
            {
                Flush();
            }            
        }
        
        public bool Delete(obj entity)
        {
            try
            {
                BeginTransaction();
                DeleteObject(entity);
                Commit();
                return true;
            }
            catch(Exception ex)
            {
                Rollback();
                throw ex;
            }
            finally
            {
                Flush();
            }
        }

        public bool DeleteAllByProperties(IEnumerable<CriteriaConditions> conditions)
        {
            try
            {
                var list = SelectAllByProperties(conditions);
                BeginTransaction();
                foreach (var item in list)
                {
                    DeleteObject(item);
                }
                Commit();
                return true;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
            finally
            {
                Flush();
            }
        }

        public bool DeleteAll()
        {
            try
            {
                var list = SelectAll();
                BeginTransaction();
                foreach (var item in list)
                {
                    DeleteObject(item);
                }
                Commit();
                return true;
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
            finally
            {
                Flush();
            }
        }

        public obj SelectById(object id)
        {
            var result = Session.Get<obj>(id);
            
            return result;
        }

        public IEnumerable<obj> SelectAll()
        {
            var result = Session.CreateCriteria(typeof(obj)).List<obj>();
            Clear();
            return result;
        }
        
        public obj SelectByProperty(string property, object value)
        {
            value = VerifyType.ConvertProperty(property, value, typeof(obj));
            var result = Session.CreateCriteria(typeof(obj)).Add(Expression.Eq(property, value)).UniqueResult<obj>();
            Clear();
            return result;
        }

        public IEnumerable<obj> SelectAllByProperty(string property, object value)
        {
            value = VerifyType.ConvertProperty(property, value, typeof(obj));
            var result = Session.CreateCriteria(typeof(obj)).Add(Expression.Eq(property, value)).List<obj>();
            Clear();
            return result;
        }

        public IEnumerable<obj> SelectAllByProperties(IEnumerable<CriteriaConditions> conditions)
        {
            var criteria = Session.CreateCriteria(typeof(obj));
            foreach (var item in conditions)
            {                
                item.Value = VerifyType.ConvertProperty(item.Property, item.Value, typeof(obj)); 
                criteria.Add(Restrictions.Eq(item.Property, item.Value));
            }
            var result = criteria.List<obj>();
            Clear();
            return result;
        }        
        
        #region "Persistênca interna"
        private void SaveObject(obj entity)
        {
            Session.SaveOrUpdate(entity);
        }

        private void DeleteObject(obj entity)
        {
            Session.Delete(entity);
        }
        #endregion
        #endregion
    }
}
