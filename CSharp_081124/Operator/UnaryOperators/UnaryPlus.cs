using System;

namespace CSharp_081124.Operators.UnaryOperators
{
    /// <summary>
    /// Public class representing a unary plus.
    /// </summary>
    public class UnaryPlus
        : UnaryOperator
    {
        /// <summary>
        /// Creates a new unary plus operator with a given operand.
        /// </summary>
        /// 
        /// <param name="operand">The operand of the unary plus operator.</param>
        public UnaryPlus( Expression operand )
            : base( operand )
        {
        }

        /// <summary>
        /// Evaluates the unary plus.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the unary plus.
        /// </returns>
        public override int Evaluate()
        {
            return (+ operand.Evaluate());
        }
    }
}
