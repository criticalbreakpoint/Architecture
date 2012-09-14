namespace Architecture.Core.PersistenceSupport
{
    public interface ICommand
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        void Execute();
    }

    public abstract class CommandBase : ICommand
    {
        #region ICommand Members

        public virtual void Execute()
        {
            Execute(UnitOfWorkBase.CurrentScope());
        }

        #endregion

        protected abstract void Execute(IContextManager manager);
    }
}