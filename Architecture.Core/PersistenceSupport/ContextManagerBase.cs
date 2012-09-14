using System;
using System.Collections.Generic;
using System.Linq;

namespace Architecture.Core.PersistenceSupport
{
    public interface IContextManager : IDisposable
    {
        /// <summary>
        /// Resolves a context object of the specified type and name (if provided).
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        TContext Resolve<TContext>(string name = null)
            where TContext : class;
    }

    public abstract class ContextManagerBase : IContextManager
    {
        private readonly Dictionary<Type, ContextNode> _contextRegistry;
        private bool _disposed;

        protected ContextManagerBase()
        {
            _contextRegistry = new Dictionary<Type, ContextNode>();
        }

        protected internal IEnumerable<object> Contexts
        {
            get
            {
                return _contextRegistry.Select(pair => pair.Value)
                    .SelectMany(node => node.Select(pair => pair.Value)
                                            .Union(new[] {node.Default}))
                    .Where(x => x != null)
                    .Distinct();
            }
        }

        #region IContextManager Members

        public void Dispose()
        {
            Dispose(true);
        }

        public TContext Resolve<TContext>(string name = null)
            where TContext : class
        {
            Type contextType = typeof (TContext);

            if (_contextRegistry.ContainsKey(contextType) == false)
            {
                _contextRegistry[contextType] = new ContextNode();
            }

            if (string.IsNullOrEmpty(name))
            {
                if (_contextRegistry[contextType].Default == null)
                {
                    _contextRegistry[contextType].Default = Create<TContext>(name);
                }

                return _contextRegistry[contextType].Default as TContext;
            }

            if (_contextRegistry[contextType].ContainsKey(name) == false)
            {
                _contextRegistry[contextType][name] = Create<TContext>(name);
            }

            return _contextRegistry[contextType][name] as TContext;
        }

        #endregion

        protected abstract TContext Create<TContext>(string name = null)
            where TContext : class;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing
                && _disposed == false)
            {
                foreach (object context in Contexts)
                {
                    var disposableContext = context as IDisposable;
                    if (disposableContext != null)
                    {
                        disposableContext.Dispose();
                    }
                }
                _disposed = true;
            }
        }

        ~ContextManagerBase()
        {
            Dispose(false);
        }

        #region Nested type: ContextNode

        private class ContextNode : Dictionary<string, object>
        {
            public ContextNode()
            {
                Default = null;
            }

            public object Default { get; set; }
        }

        #endregion
    }
}