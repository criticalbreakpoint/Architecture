using System;
using System.Collections.Generic;
using Architecture.Core.Exceptions;

namespace Architecture.Core.PersistenceSupport
{
    public class UnitOfWork : IUnitOfWork
    {
        [ThreadStatic] private static Stack<UnitOfWork> _instances;
        private readonly ContextScope _scope;
        private bool _disposed;

        public UnitOfWork()
        {
            _scope = new ContextScope();
            Instances().Push(this);
        }

        #region IUnitOfWork Members

        public void SubmitChanges()
        {
            foreach (IContext context in Scope().GetAll())
            {
                context.SubmitChanges();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        ~UnitOfWork()
        {
            Dispose(false);
        }

        protected static Stack<UnitOfWork> Instances()
        {
            if (_instances == null)
            {
                _instances = new Stack<UnitOfWork>();
            }

            return _instances;
        }

        /// <summary>
        /// Gets the current UnitOfWork context scope.
        /// </summary>
        /// <returns></returns>
        public static ContextScope CurrentScope()
        {
            if (Instances().Count == 0)
            {
                throw new PersistenceException(
                    "There is no scope currently defined. Create a new unit of work before accessing the current scope.");
            }

            return Instances()
                .Peek()
                .Scope();
        }

        protected ContextScope Scope()
        {
            return _scope;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing
                && _disposed == false)
            {
                Instances().Pop();
                _scope.Dispose();
                _disposed = true;
            }
        }
    }
}