using Architecture.Core.PersistenceSupport;
using Architecture.Tests.Core.PersistenceSupport.TestObjects;
using NUnit.Framework;

namespace Architecture.Tests.Core.PersistenceSupport
{
    [TestFixture]
    public class UnitOfWorkFixture
    {
        [Test]
        public void CanGetCorrectNestedCurrentScope()
        {
            using (var level1 = new TestUnitOfWork())
            {
                Assert.AreEqual(level1, UnitOfWorkBase.CurrentScope());
                using (var level2 = new TestUnitOfWork())
                {
                    Assert.AreEqual(level2, UnitOfWorkBase.CurrentScope());
                    using (var level3 = new TestUnitOfWork())
                    {
                        Assert.AreEqual(level3, UnitOfWorkBase.CurrentScope());
                    }
                    Assert.AreEqual(level2, UnitOfWorkBase.CurrentScope());
                }
                Assert.AreEqual(level1, UnitOfWorkBase.CurrentScope());
            }
        }
    }
}