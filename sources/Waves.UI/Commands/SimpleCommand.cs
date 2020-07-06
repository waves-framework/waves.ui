using System;
using System.Windows.Input;

namespace Waves.UI.Commands
{
    /// <summary>
    /// Simple command without "CanExecute" handling.
    /// </summary>
    public class SimpleCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        /// <summary>
        ///     Creates new instance of simple command.
        /// </summary>
        /// <param name="execute">Execute action.</param>
        /// <param name="canExecute">Can execute delegate.</param>
        public SimpleCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        /// <inheritdoc />
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}