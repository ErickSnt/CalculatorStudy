using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Data;
using System.Text;
using NCalc;
using System.Linq.Expressions;
using System.Diagnostics;

namespace CalculatorStudy
{
    public partial class MainPageViewModel : ObservableObject
    {
        private StringBuilder _expressionBuilder = new StringBuilder();

        [ObservableProperty]
        private string _expressionDisplay = string.Empty;

        [ObservableProperty]
        private string _resultDisplay = string.Empty;

        [RelayCommand]
        public void HandleButtonPress(string buttonText)
        {
            Debug.WriteLine($"Button Pressed: {buttonText}"); // Log para o botão pressionado

            if (buttonText == "C")
            {
                _expressionBuilder.Clear();
                ResultDisplay = string.Empty;
                Debug.WriteLine("Clearing expression and result.");
            }
            else if (buttonText == "Del")
            {
                if (_expressionBuilder.Length > 0)
                {
                    _expressionBuilder.Remove(_expressionBuilder.Length - 1, 1);
                    Debug.WriteLine($"Deleted last character. Current expression: {_expressionBuilder}");
                }
            }
            else if (buttonText == "=")
            {
                Debug.WriteLine("Equals button pressed.");
                try
                {
                    var expressionString = _expressionBuilder.ToString()
                                                 .Replace("÷", "/")
                                                 .Replace("x", "*")
                                                 .Replace(",", ".");

                    // Substituir expressões como "50%" por "50/100"
                    expressionString = System.Text.RegularExpressions.Regex.Replace(expressionString, @"(\d+)%(\d+)", "($1/100)*$2");


                    Debug.WriteLine($"Expression to evaluate: {expressionString}");

                    var expression = new NCalc.Expression(expressionString);
                    var result = expression.Evaluate();
                    ResultDisplay = result.ToString();
                    Debug.WriteLine($"Evaluation result: {ResultDisplay}");
                }
                catch (Exception ex)
                {
                    ResultDisplay = "Erro";
                    Debug.WriteLine($"Error evaluating expression: {ex.Message}");
                }
            }
            else
            {
                _expressionBuilder.Append(buttonText);
                Debug.WriteLine($"Updated expression: {_expressionBuilder}");
            }

            ExpressionDisplay = _expressionBuilder.ToString();
        }
    }
}