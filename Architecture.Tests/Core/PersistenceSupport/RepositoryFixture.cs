using System.Linq;
using Architecture.Core.Exceptions;
using Architecture.Tests.Core.PersistenceSupport.TestObjects;
using NUnit.Framework;

namespace Architecture.Tests.Core.PersistenceSupport
{
    [TestFixture]
    public class RepositoryFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _repository = new TestRepository<EntityObject>();
            _entity = new EntityObject
                {
                    Name = "Candy",
                    Description = "Delicious"
                };
        }

        #endregion

        private TestRepository<EntityObject> _repository;
        private EntityObject _entity;

        [Test]
        public void CanDeleteWithinUnitOfWorkScope()
        {
            using (new TestUnitOfWork())
            {
                _repository.Save(_entity);

                _repository.Delete(_entity);

                _entity = _repository.Query()
                    .SingleOrDefault(x => x.Id == _entity.Id);

                Assert.IsNull(_entity);
            }
        }

        [Test]
        public void CanQueryWithinUnitOfWorkScope()
        {
            using (new TestUnitOfWork())
            {
                _repository.Save(_entity);

                _entity = _repository.Query()
                    .SingleOrDefault(x => x.Id == _entity.Id);

                Assert.IsNotNull(_entity);
            }
        }

        [Test]
        public void CanSaveOrUpdateWithinUnitOfWorkScope()
        {
            using (new TestUnitOfWork())
            {
                _repository.Save(_entity);

                _repository.Update(_entity);

                Assert.That(_entity.Id,
                            Is.GreaterThan(0));
            }
        }

        [Test]
        public void CaNotQueryDeleteSaveOrUpdateOutsideUnitOfWorkScope()
        {
            Assert.Throws<PersistenceException>(
                () => _repository.Save(_entity));

            Assert.Throws<PersistenceException>(
                () => _repository.Update(_entity));

            Assert.Throws<PersistenceException>(
                () => _repository.Delete(_entity));

            Assert.Throws<PersistenceException>(
                () => _entity = _repository.Query()
                              .SingleOrDefault(x => x.Id == _entity.Id));
        }
    }
}