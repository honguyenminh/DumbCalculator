using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DumbCalculator.Utils;
using OneOf;
using Windows.ApplicationModel.Contacts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DumbCalculator
{
    public class InPlaceOperation
    {
        public static readonly InPlaceOperation Zero = new() { Target = 0 };
        public static readonly InPlaceOperation Empty = new() { Target = 0, Type = InPlaceOperationType.Empty };

        public required OneOf<InPlaceOperation, decimal> Target { get; set; }
        public InPlaceOperationType Type { get; set; } = InPlaceOperationType.None;

        public decimal Value
        {
            get
            {
                decimal value = 0;
                Target.Switch(
                    ops => value = ops.Value,
                    num => value = num
                );

                return Type switch
                {
                    InPlaceOperationType.Inverse => 1 / value,
                    InPlaceOperationType.Square => value * value,
                    InPlaceOperationType.SquareRoot => MathUtils.Sqrt(value),
                    InPlaceOperationType.None => value,
                    InPlaceOperationType.Empty => 0,
                    _ => throw new InvalidOperationException("Operation type enum out of range")
                };
            }
        }

        public override string ToString()
        {
            return Type switch
            {
                InPlaceOperationType.Inverse => $"1/({TargetToString()})",
                InPlaceOperationType.Square => $"sqr({TargetToString()})",
                InPlaceOperationType.SquareRoot => $"sqrt({TargetToString()})",
                InPlaceOperationType.None => TargetToString(),
                InPlaceOperationType.Empty => string.Empty,
                _ => throw new InvalidOperationException("Operation type enum out of range")
            };
        }

        public static explicit operator decimal(InPlaceOperation op) => op.Value;
        public static implicit operator InPlaceOperation(decimal x)
        {
            if (x == 0) return Zero;
            return new() { Target = x };
        }

        private string TargetToString()
        {
            if (Target.IsT0) return Target.AsT0.ToString();
            return Target.AsT1.ToString();
        }
    }
    public enum InPlaceOperationType
    {
        Empty, None, Inverse, Square, SquareRoot
    }
}
