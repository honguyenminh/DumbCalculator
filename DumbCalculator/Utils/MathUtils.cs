using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumbCalculator.Utils
{
    internal static class MathUtils
    {
        /// <summary>
        /// Calculate the square root of a <see langword="decimal"/> using Newton's method with very high precision.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="epsilon">Precision for the calculation</param>
        /// <exception cref="OverflowException">If x is negative</exception>
        public static decimal Sqrt(decimal x, decimal epsilon = 0.0M)
        {
            if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

            decimal current = (decimal)Math.Sqrt((double)x), previous;
            do
            {
                previous = current;
                if (previous == 0.0M) return 0;
                current = (previous + x / previous) / 2;
            }
            while (Math.Abs(previous - current) > epsilon);
            return current;
        }
    }
}
