using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;

using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DumbCalculator
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WinUIEx.WindowEx
    {
        public MainWindowViewModel ViewModel { get; set; } = new();

        public MainWindow()
        {
            ExtendsContentIntoTitleBar = true;
            this.InitializeComponent();
            foreach (var childElement in ButtonGrid.Children)
            {
                if (childElement is not Button) continue;
                var button = (Button)childElement;
                if (button.Content is not string) continue;
                bool isNumeric = IsNumericRegex().IsMatch((string)button.Content);
                if (isNumeric)
                    button.Click += NumberButton_Clicked;
            }
        }

        private void NumberButton_Clicked(object sender, RoutedEventArgs e)
        {
            Button button = (sender as Button)!;
            string number = (button.Content as string)!;
            ViewModel.Input += number;
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Input.Length == 0) return;
            if (ViewModel.Input[^1] == '.') _decimalPointExists = false;
            ViewModel.Input = ViewModel.Input[..^1];
        }

        private void ClearEnterButton_Click(object sender, RoutedEventArgs e)
        {
            _decimalPointExists = false;
            ViewModel.Input = string.Empty;
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearEnterButton_Click(sender, e);
            ViewModel.PendingOperation = new();
        }

        private bool _decimalPointExists = false;
        private void DecimalButton_Click(object? sender, RoutedEventArgs? e)
        {
            if (_decimalPointExists) return;
            _decimalPointExists = true;
            if (ViewModel.Input.Length == 1) 
                ViewModel.Input = "0";

            ViewModel.Input += '.';
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: fix the input after equal bug
            var currentOp = ViewModel.PendingOperation;
            var input = decimal.Parse(ViewModel.Input);
            currentOp.RightOperand = input;
            ViewModel.PendingOperation = currentOp;
            ClearEnterButton_Click(sender, e);
            ViewModel.InputBoxText = currentOp.Calculate().ToString();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            OperationKeyPressed(OperationType.Addition);
        }
        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            OperationKeyPressed(OperationType.Subtraction);
        }
        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            OperationKeyPressed(OperationType.Multiplication);
        }
        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            OperationKeyPressed(OperationType.Division);
        }

        private void OperationKeyPressed(OperationType op)
        {
            EqualButton_Click(null!, null!); // dont do this
            var currentOp = ViewModel.PendingOperation;
            currentOp.LeftOperand = currentOp.Calculate();
            currentOp.RightOperand = InPlaceOperation.Empty;
            currentOp.Type = op;

            ViewModel.PendingOperation = currentOp;
        }

        [GeneratedRegex(@"^\d$")]
        private static partial Regex IsNumericRegex();
    }
}
