using System.Linq;

namespace Architecture.Core.PersistenceSupport
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Saves a new entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Save(T entity);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Queries the entity store.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Query();
    }

    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class
    {
        #region IRepository<T> Members

        public void Save(T entity)
        {
            Save(entity, UnitOfWorkBase.CurrentScope());
        }

        public void Update(T entity)
        {
            Update(entity, UnitOfWorkBase.CurrentScope());
        }

        public void Delete(T entity)
        {
            Delete(entity, UnitOfWorkBase.CurrentScope());
        }

        public IQueryable<T> Query()
        {
            return Query(UnitOfWorkBase.CurrentScope());
        }

        #endregion

        protected abstract void Save(T entity, IContextManager manager);

        protected abstract void Update(T entity, IContextManager manager);

        protected abstract void Delete(T entity, IContextManager manager);

        protected abstract IQueryable<T> Query(IContextManager manager);
    }
}