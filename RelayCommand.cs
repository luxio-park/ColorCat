using System;
using System.Windows.Input;

namespace Luxio
{
    class RelayCommand : ICommand
    {
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            MyExecute = execute;
            MyCanExcute = canExecute;
        }

        private Action<object> MyExecute;
        private Predicate<object> MyCanExcute;


        public bool CanExecute(object parameter)
        {
            if (MyCanExcute != null)
            {
                return MyCanExcute(parameter);
            }
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            MyExecute(parameter);
        }
    }
}