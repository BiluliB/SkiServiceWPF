using System.Windows.Input;

namespace SkiServiceWPF.Commands
{
    /// <summary>
    /// A command that executes an Action, with optional condition checking
    /// </summary>
    public class RelayCommand : ICommand
    {
        // Holds the delegate for the execute action
        private readonly Action _execute;

        // Holds the delegate for the execute action
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class
        /// </summary>
        /// <param name="execute">The Action to execute</param>
        /// <param name="canExecute">The function to determine if the action can be executed</param>
        /// <exception cref="ArgumentNullException">Thrown if 'execute' is null</exception>
        #region RelayCommand
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        #endregion

        /// <summary>
        /// Raised when changes occur that affect whether the command should execute
        /// </summary>
        #region EventHandler
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        /// <summary>
        /// Determines whether the command can execute in its current state
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        #region CanExecute
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }
        #endregion

        /// <summary>
        /// Executes the RelayCommand on the current command target
        /// </summary>
        /// <param name="parameter"></param>
        #region Execute
        public void Execute(object parameter)
        {
            _execute();
        }
        #endregion
    }
}
