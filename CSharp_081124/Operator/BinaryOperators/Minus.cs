using System;

namespace CSharp_081124.Operators.BinaryOperators
{
    /// <summary>
    /// Public class representing a (binary) minus.
    /// </summary>
    public class Minus
        : BinaryOperator
    {
        /// <summary>
        /// Evaluates the (binary) minus.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the (binary) minus.
        /// </returns>
        public override int Evaluate()
        {
            return (leftOperand.Evaluate() - rightOperand.Evaluate());
        }
    }
}
