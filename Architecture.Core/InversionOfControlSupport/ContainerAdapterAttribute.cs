using System;

namespace Architecture.Core.InversionOfControlSupport
{
    /// <summary>
    /// Defines the ContainerAdapter to use for the current application.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public sealed class ContainerAdapterAttribute : Attribute
    {
        private readonly IContainerAdapter _containerAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerAdapterAttribute"/> class.
        /// </summary>
        /// <param name="containerAdapterType">Type of the container adapter.</param>
        /// <param name="args">The arguments needed to create a new instance of the container adapter.</param>
        public ContainerAdapterAttribute(Type containerAdapterType, params object[] args)
        {
            _containerAdapter = Activator.CreateInstance(containerAdapterType, args) as IContainerAdapter;
        }

        /// <summary>
        /// The container adapter.
        /// </summary>
        public IContainerAdapter ContainerAdapter
        {
            get { return _containerAdapter; }
        }
    }
}