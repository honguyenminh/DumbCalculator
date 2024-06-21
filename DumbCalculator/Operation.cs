using DumbCalculator.Utils;

namespace DumbCalculator
{
    public struct Operation
    {
        public Operation() {}

        public InPlaceOperation LeftOperand { get; set; } = InPlaceOperation.Empty;
        public InPlaceOperation RightOperand { get; set; } = InPlaceOperation.Empty;

        public OperationType Type { get; set; }

        public readonly decimal Calculate()
        {
            return Type switch
            {
                OperationType.Addition => LeftOperand.Value + RightOperand.Value,
                OperationType.Subtraction => LeftOperand.Value - RightOperand.Value,
                OperationType.Multiplication => LeftOperand.Value * RightOperand.Value,
                OperationType.Division => LeftOperand.Value / RightOperand.Value,
                OperationType.None => RightOperand.Value,
                _ => throw new InvalidOperationException("Type enum not supported"),
            };
        }

        public override readonly string ToString()
        {
            if (Type is OperationType.None) return string.Empty;
            return $"{LeftOperand} {Type.ToSymbolString()} {RightOperand}".Trim();
        }
    }
    public enum OperationType
    {
        None = 0, Addition, Subtraction, Multiplication, Division
    }
}
