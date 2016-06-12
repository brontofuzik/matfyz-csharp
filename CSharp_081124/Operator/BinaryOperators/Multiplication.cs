using System;

namespace CSharp_081124.Operators.BinaryOperators
{
    /// <summary>
    /// Public class representing a (binary) multiplication.
    /// </summary>
    class Multiplication
        : BinaryOperator
    {
        /// <summary>
        /// Evaluates the (binary) multiplication.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the (binary) multiplication.
        /// </returns>
        public override int Evaluate()
        {
            return (leftOperand.Evaluate() * rightOperand.Evaluate());
        }
    }
}
