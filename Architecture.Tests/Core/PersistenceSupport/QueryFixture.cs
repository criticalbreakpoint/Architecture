using System.Collections.Generic;
using System.Linq;
using Architecture.Core.Exceptions;
using Architecture.Tests.Core.PersistenceSupport.TestObjects;
using NUnit.Framework;

namespace Architecture.Tests.Core.PersistenceSupport
{
    [TestFixture]
    public class QueryFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _query = new TestQuery();
        }

        #endregion

        private TestQuery _query;

        [Test]
        public void CanExecuteWithinUnitOfWorkScope()
        {
            using (new TestUnitOfWork())
            {
                _query.Id = 1;

                List<EntityObject> result = _query
                    .Execute()
                    .ToList();

                Assert.IsNotNull(result);
            }
        }

        [Test]
        public void CanNotExecuteOutsideUnitOfWorkScope()
        {
            _query.Id = 1;
            List<EntityObject> result = null;

            Assert.Throws<PersistenceException>(
                () => result = _query.Execute().ToList());
            Assert.IsNull(result);
        }
    }
}