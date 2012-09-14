using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Architecture.Tests.Core.PersistenceSupport.TestObjects
{
    public class NHibernateContext : IDisposable
    {
        private readonly ISession _session;
        private bool _disposed;

        public NHibernateContext()
        {
            _session = NHibernateHelper.OpenSession();
            _session.BeginTransaction();
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        public void SaveChanges()
        {
            _session.Transaction.Commit();
            _session.BeginTransaction();
        }

        public void SaveOrUpdate<T>(T entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public void Delete<T>(T entity)
        {
            _session.Delete(entity);
        }

        public IQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed == false
                && disposing)
            {
                _session.Dispose();
                _disposed = true;
            }
        }

        ~NHibernateContext()
        {
            Dispose(false);
        }
    }
}