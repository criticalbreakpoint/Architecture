using Architecture.Core.PersistenceSupport;

namespace Architecture.Tests.Core.PersistenceSupport.TestObjects
{
    public class TestCommand : CommandBase
    {
        public EntityObject EntityObject { get; set; }

        protected override void Execute(IContextManager manager)
        {
            manager.Resolve<NHibernateContext>()
                .SaveOrUpdate(EntityObject);
        }
    }
}