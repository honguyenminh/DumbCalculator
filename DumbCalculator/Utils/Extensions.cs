using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumbCalculator.Utils
{
    internal static class Extensions
    {
        public static string ToSymbolString(this OperationType operation) {
            return operation switch
            {
                OperationType.Addition => "+",
                OperationType.Subtraction => "−",
                OperationType.Multiplication => "×",
                OperationType.Division => "÷",
                OperationType.None => string.Empty,
                _ => throw new ArgumentOutOfRangeException(nameof(operation), "Enum value out of range")
            };
        }
    }
}
