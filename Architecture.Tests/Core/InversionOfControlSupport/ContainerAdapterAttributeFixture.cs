using System.Linq;
using System.Reflection;
using Architecture.Core.InversionOfControlSupport;
using Architecture.Tests.Core.InversionOfControlSupport.TestObjects;
using NUnit.Framework;

[assembly: ContainerAdapter(typeof (TestContainerAdapter))]

namespace Architecture.Tests.Core.InversionOfControlSupport
{
    [TestFixture]
    public class ContainerAdapterAttributeFixture
    {
        [Test]
        public void CanGetAttributeContainerAdapter()
        {
            ContainerAdapterAttribute attribute = Assembly.GetAssembly(typeof (ContainerAdapterAttributeFixture))
                .GetCustomAttributes(typeof (ContainerAdapterAttribute), false)
                .OfType<ContainerAdapterAttribute>()
                .SingleOrDefault();

            Assert.IsNotNull(attribute);
            Assert.IsNotNull(attribute.ContainerAdapter);
            Assert.IsInstanceOf<TestContainerAdapter>(
                attribute.ContainerAdapter);
        }
    }
}