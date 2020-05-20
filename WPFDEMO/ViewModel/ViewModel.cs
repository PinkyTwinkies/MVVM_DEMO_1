using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFDEMO.ViewModel
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private String input;

        public String Input
        {
            get { return this.input; }
            set
            {
                this.input = value;
                RaisePropertyChanged();
            }
        }

        private String output;

        public String Output
        {
            get { return this.output; }
            set
            {
                this.output = value;
                RaisePropertyChanged();
            }
        }

        public ICommand ChangeText { get; set; }

        public ViewModel()
        {
            ChangeText = new RelayCommand(e => { this.Output = this.Input; },
                o =>
                {
                    return true;
                });
        }


        class RelayCommand : ICommand
        {
            private Action<object> execute;
            private Func<object, bool> canExecute;

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            {
                this.execute = execute;
                this.canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return this.canExecute == null || this.canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                this.execute(parameter);
            }
        }

    }
}
