using System.Linq;
using Architecture.Core.PersistenceSupport;

namespace Architecture.Tests.Core.PersistenceSupport.TestObjects
{
    public class TestRepository<T> : RepositoryBase<T>
        where T : class
    {
        protected override void Save(T entity, IContextManager manager)
        {
            manager.Resolve<NHibernateContext>()
                .SaveOrUpdate(entity);
        }

        protected override void Update(T entity, IContextManager manager)
        {
            manager.Resolve<NHibernateContext>()
                .SaveOrUpdate(entity);
        }

        protected override void Delete(T entity, IContextManager manager)
        {
            manager.Resolve<NHibernateContext>()
                .Delete(entity);
        }

        protected override IQueryable<T> Query(IContextManager manager)
        {
            return manager.Resolve<NHibernateContext>()
                .Query<T>();
        }
    }
}