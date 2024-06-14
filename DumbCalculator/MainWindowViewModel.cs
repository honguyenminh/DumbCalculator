using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DumbCalculator
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _input = "0";
        public string Input 
        {
            get => _input;
            set 
            {
                value = value.TrimStart('0');
                if (value.Length == 0) value = "0";
                _input = value;
                OnPropertyChanged(); 
            } 
        }

        // todo: switch to operation class and convert from that
        private string _pendingOperation = string.Empty;
        public string PendingOperation 
        { 
            get => _pendingOperation; 
            set 
            {
                _pendingOperation = value;
                OnPropertyChanged(); 
            } 
        }

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
