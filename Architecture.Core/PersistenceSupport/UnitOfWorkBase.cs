using System;
using System.Collections.Generic;
using Architecture.Core.Exceptions;

namespace Architecture.Core.PersistenceSupport
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Submits all the context changes in the current unit of work.
        /// </summary>
        void SubmitChanges();
    }

    public abstract class UnitOfWorkBase : ContextManagerBase, IUnitOfWork
    {
        [ThreadStatic] private static Stack<UnitOfWorkBase> _instances;
        private bool _disposed;

        protected UnitOfWorkBase()
        {
            Instances.Push(this);
        }

        protected static Stack<UnitOfWorkBase> Instances
        {
            get
            {
                if (_instances == null)
                {
                    _instances = new Stack<UnitOfWorkBase>();
                }

                return _instances;
            }
        }

        #region IUnitOfWork Members

        public void SubmitChanges()
        {
            foreach (object context in Contexts)
            {
                SubmitChanges(context);
            }
        }

        #endregion

        protected abstract void SubmitChanges(object context);

        public static UnitOfWorkBase CurrentScope()
        {
            if (Instances.Count == 0)
            {
                throw new PersistenceException(
                    "There is no contextScope currently defined. Create a new unit of work before accessing the current contextScope.");
            }

            return Instances.Peek();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing
                && _disposed == false)
            {
                Instances.Pop();
                _disposed = true;
            }

            base.Dispose(disposing);
        }
    }
}