namespace Architecture.Core.InversionOfControlSupport
{
    public interface IContainerAdapter
    {
        /// <summary>
        /// Gets an instance of type T.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <returns>Instance of T</returns>
        T GetInstance<T>() where T : class;

        /// <summary>
        /// Gets a named instance of type T.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>Instance of T</returns>
        T GetInstance<T>(string name) where T : class;

        /// <summary>
        /// Tries to get an instance of type T.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        T TryGetInstance<T>() where T : class;

        /// <summary>
        /// Tries to get a named instance of type T.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="name">The name.</param>
        T TryGetInstance<T>(string name) where T : class;
    }
}