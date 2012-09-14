using System;
using Architecture.Core.PersistenceSupport;

namespace Architecture.Tests.Core.PersistenceSupport.TestObjects
{
    public class TestUnitOfWork : UnitOfWorkBase
    {
        protected override void SubmitChanges(object context)
        {
            if (context is NHibernateContext)
            {
                var testContext = context as NHibernateContext;
                testContext.SaveChanges();
            }
        }

        protected override TContext Create<TContext>(string name = null)
        {
            return Activator.CreateInstance<TContext>();
        }
    }
}