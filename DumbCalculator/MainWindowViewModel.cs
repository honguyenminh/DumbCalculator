using System.ComponentModel;
using System.Runtime.CompilerServices;

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
                if (!value.StartsWith("0."))
                    value = value.TrimStart('0');
                if (value.Length == 0) 
                    value = "0";
                _input = value;
                InputBoxText = value;
                OnPropertyChanged(); 
            }
        }
        private string _inputBoxText = string.Empty;
        public string InputBoxText
        {
            get => _inputBoxText;
            set
            {
                _inputBoxText = value;
                OnPropertyChanged();
            }
        }

        private Operation _pendingOperation = new();
        public Operation PendingOperation
        {
            get => _pendingOperation;
            set
            {
                _pendingOperation = value;
                OnPropertyChanged();
            }
        }

        private bool _isShowingResult = false;
        public bool IsShowingResult 
        { 
            get => _isShowingResult; 
            set
            {
                _isShowingResult = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
