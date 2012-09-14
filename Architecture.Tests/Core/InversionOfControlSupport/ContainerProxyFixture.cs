using Architecture.Core.InversionOfControlSupport;
using Architecture.Tests.Core.InversionOfControlSupport.TestObjects;
using NUnit.Framework;

namespace Architecture.Tests.Core.InversionOfControlSupport
{
    [TestFixture]
    public class ContainerProxyFixture
    {
        [Test]
        public void CanGetConfiguredContainerAdapter()
        {
            Assert.IsNotNull(ContainerProxy.ContainerAdapter);
            Assert.IsInstanceOf<TestContainerAdapter>(
                ContainerProxy.ContainerAdapter);
        }
    }
}