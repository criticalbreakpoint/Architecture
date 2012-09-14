using System.Collections.Generic;
using System.Linq;
using Architecture.Core.PersistenceSupport;

namespace Architecture.Tests.Core.PersistenceSupport.TestObjects
{
    public class TestQuery : QueryBase<EntityObject>
    {
        public int Id { get; set; }

        protected override IEnumerable<EntityObject> Execute(IContextManager manager)
        {
            return manager.Resolve<NHibernateContext>().Query<EntityObject>()
                .Where(x => x.Id == Id)
                .AsEnumerable();
        }
    }
}