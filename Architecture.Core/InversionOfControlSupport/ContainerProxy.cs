using System;
using System.Linq;
using Architecture.Core.Exceptions;

namespace Architecture.Core.InversionOfControlSupport
{
    /// <summary>
    /// Provides static access to methods for the currently configured DependencyInjection Container
    /// </summary>
    public static class ContainerProxy
    {
        #region Private Static Fields

        private static volatile IContainerAdapter _containerAdapter;
        private static readonly object SyncRoot = new object();

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Gets the configured container adapter.
        /// </summary>
        public static IContainerAdapter ContainerAdapter
        {
            get
            {
                if (_containerAdapter == null)
                {
                    lock (SyncRoot)
                    {
                        if (_containerAdapter == null)
                        {
                            _containerAdapter = FindContainerAdapter();
                        }
                    }
                }

                return _containerAdapter;
            }
        }

        #endregion

        #region Public Proxy Methods

        /// <summary>
        /// Gets an instance of type T.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <returns>Instance of T</returns>
        public static T GetInstance<T>()
            where T : class
        {
            CheckConfiguration();
            return ContainerAdapter.GetInstance<T>();
        }

        /// <summary>
        /// Gets a named instance of type T.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>Instance of T</returns>
        public static T GetInstance<T>(string name)
            where T : class
        {
            CheckConfiguration();
            return ContainerAdapter.GetInstance<T>();
        }

        /// <summary>
        /// Tries to get an instance of type T.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        public static T TryGetInstance<T>()
            where T : class
        {
            CheckConfiguration();
            return ContainerAdapter.TryGetInstance<T>();
        }

        /// <summary>
        /// Tries to get a named instance of type T.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="name">The name.</param>
        public static T TryGetInstance<T>(string name)
            where T : class
        {
            CheckConfiguration();
            return ContainerAdapter.TryGetInstance<T>(name);
        }

        public static void Configure()
        {
            _containerAdapter = FindContainerAdapter();
        }

        #endregion

        #region Private Helper Methods

        private static void CheckConfiguration()
        {
            if (ContainerAdapter == null)
            {
                throw new DependencyInjectionException(
                    "The DependencyInjection configuration is invalid. There must be a ContainerAdapter defined for the current application.");
            }
        }

        private static IContainerAdapter FindContainerAdapter()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Select(assembly => assembly.GetCustomAttributes(typeof (ContainerAdapterAttribute), false)
                                        .SingleOrDefault(a => a is ContainerAdapterAttribute))
                .OfType<ContainerAdapterAttribute>()
                .Select(attribute => attribute.ContainerAdapter)
                .FirstOrDefault(c => c != null);
        }

        #endregion
    }
}