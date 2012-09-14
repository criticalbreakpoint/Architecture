using System.Collections.Generic;

namespace Architecture.Core.PersistenceSupport
{
    public interface IQuery<out T>
        where T : class
    {
        /// <summary>
        /// Executes this query.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Execute();
    }

    public abstract class QueryBase<T> : IQuery<T>
        where T : class
    {
        #region IQuery<T> Members

        public IEnumerable<T> Execute()
        {
            return Execute(UnitOfWorkBase.CurrentScope());
        }

        #endregion

        protected abstract IEnumerable<T> Execute(IContextManager manager);
    }
}