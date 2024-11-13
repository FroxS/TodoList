using Prism.Commands;

namespace TodoList.Services
{
    public delegate void ExeptionArgs(Exception ex);

    public class DelegateCommandEx : DelegateCommand
    {
        #region Private properties

        #endregion

        #region Public properties

        public static event ExeptionArgs ExeptionArgs;

        #endregion

        #region Constructors

        public DelegateCommandEx(Action executeMethod) : base(executeMethod)
        {

        }

        public DelegateCommandEx(Action executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
        }

        #endregion

        #region Methods


        protected override void Execute(object parameter)
        {
            try
            {
                base.Execute(parameter);
            }catch(Exception ex)
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