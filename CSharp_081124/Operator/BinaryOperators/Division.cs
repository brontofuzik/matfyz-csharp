using System;

namespace CSharp_081124.Operators.BinaryOperators
{
    /// <summary>
    /// Public class representing a (binary) division.
    /// </summary>
    public class Division
        : BinaryOperator
    {
        /// <summary>
        /// Evaluates the (binary) division.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the (binary) division.
        /// </returns>
        public override int Evaluate()
        {
            return (leftOperand.Evaluate() / rightOperand.Evaluate());
        }
    }
}
