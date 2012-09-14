using Architecture.Core.Exceptions;
using Architecture.Tests.Core.PersistenceSupport.TestObjects;
using NUnit.Framework;

namespace Architecture.Tests.Core.PersistenceSupport
{
    [TestFixture]
    public class CommandFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _command = new TestCommand();
        }

        #endregion

        private TestCommand _command;

        [Test]
        public void CanExecuteWithinUnitOfWorkScope()
        {
            using (new TestUnitOfWork())
            {
                _command.EntityObject = new EntityObject
                    {
                        Name = "Candy",
                        Description = "Delicious"
                    };

                _command.Execute();

                Assert.That(_command.EntityObject.Id,
                            Is.GreaterThan(0));
            }
        }

        [Test]
        public void CanNotExecuteOutsideUnitOfWorkScope()
        {
            _command.EntityObject = new EntityObject
                {
                    Name = "Candy",
                    Description = "Delicious"
                };

            Assert.Throws<PersistenceException>(
                () => _command.Execute());
            Assert.That(_command.EntityObject.Id,
                        Is.EqualTo(0));
        }
    }
}