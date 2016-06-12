using System;

namespace CSharp_081124.Operators.BinaryOperators
{
    /// <summary>
    /// Public class representing a (binary) plus.
    /// </summary>
    public class Plus
        : BinaryOperator
    {
        /// <summary>
        /// Evaluates the (binary) plus.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the (binary) plus.
        /// </returns>
        public override int Evaluate()
        {
            return (leftOperand.Evaluate() + rightOperand.Evaluate());
        }
    }
}
