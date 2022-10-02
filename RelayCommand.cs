using System;
using System.Windows.Input;

namespace WPF_Test
{
    public class RelayCommand: ICommand
    {
        private Action<object> execute;
        private Func<object, bool>? canExecute;
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        
        public bool CanExecute(object? parameter)
        {
            #nullable disable
            return this.canExecute == null || this.canExecute(parameter);
            #nullable enable
        }

        public void Execute(object? parameter)
        {
            #nullable disable
            this.execute(parameter);
            #nullable enable
        }
        
    }
}