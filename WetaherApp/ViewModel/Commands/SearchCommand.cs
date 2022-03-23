using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.ViewModel;

namespace WetaherApp.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherVM VM { get; set; }
        public SearchCommand(WeatherVM weatherVM)
        {
            VM = weatherVM;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.MakeQuery();
        }
    }
}
