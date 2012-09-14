using System;
using System.Runtime.Serialization;

namespace Architecture.Core.Exceptions
{
    [Serializable]
    public class DependencyInjectionException : Exception
    {
        public DependencyInjectionException()
        {
        }

        public DependencyInjectionException(string message) : base(message)
        {
        }

        public DependencyInjectionException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DependencyInjectionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}