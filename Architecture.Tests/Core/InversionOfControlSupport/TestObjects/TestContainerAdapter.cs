using System;
using Architecture.Core.InversionOfControlSupport;

namespace Architecture.Tests.Core.InversionOfControlSupport.TestObjects
{
    public class TestContainerAdapter : IContainerAdapter
    {
        #region IContainerAdapter Members

        public T GetInstance<T>() where T : class
        {
            return Activator.CreateInstance<T>();
        }

        public T GetInstance<T>(string name) where T : class
        {
            return Activator.CreateInstance<T>();
        }

        public T TryGetInstance<T>() where T : class
        {
            return Activator.CreateInstance<T>();
        }

        public T TryGetInstance<T>(string name) where T : class
        {
            return Activator.CreateInstance<T>();
        }

        #endregion
    }
}