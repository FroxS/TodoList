using Prism.Commands;

namespace TodoList.Services
{
    public delegate void ExeptionArgs(Exception ex);

    public class DelegateCommandEx : DelegateCommand
    {

        #region Fields
        private Action _executeMethod;
        private Action<object> _executeMethodParams;
        #endregion

        #region Public properties

        public static event ExeptionArgs ExeptionArgs;

        #endregion

        #region Constructors

        public DelegateCommandEx(Action executeMethod) : base(() => { })
        {
            _executeMethod = executeMethod;
        }

        public DelegateCommandEx(Action<object> executeMethod) : base(() => { })
        {
            _executeMethodParams = executeMethod;
        }

        public DelegateCommandEx(Action<object> executeMethod, Func<bool> canExecuteMethod) : base(() => { }, canExecuteMethod)
        {
            _executeMethodParams = executeMethod;
        }

        public DelegateCommandEx(Action executeMethod, Func<bool> canExecuteMethod) : base(() => { }, canExecuteMethod)
        {
            _executeMethod = executeMethod;
        }

        #endregion

        #region Methods

        public void Invoke(object parameter)
        {
            Execute(parameter);
        }

        protected override void Execute(object parameter)
        {
            try
            {
                _executeMethod?.Invoke();
                _executeMethodParams?.Invoke(parameter);
                //base.Execute(parameter);
            }
            catch(Exception ex)
            {
                ExeptionArgs?.Invoke(ex);
            }
            
        }

        protected override bool CanExecute(object parameter)
        {
            try
            {
                return base.CanExecute(parameter);
            }
            catch (Exception ex)
            {
                ExeptionArgs?.Invoke(ex);
                return false;
            }
            
        }

        #endregion
    }
}