using System;

namespace CSharp_081124.Operators.UnaryOperators
{
    /// <summary>
    /// Public class representing a unary minus.
    /// </summary>
    public class UnaryMinus
        : UnaryOperator
    {
        /// <summary>
        /// Creates a new unary minus operator with a given operand.
        /// </summary>
        /// 
        /// <param name="operand">The operand of the unary minus operator.</param>
        public UnaryMinus( Expression operand )
            : base( operand )
        {
        }

        /// <summary>
        /// Evaluates the unary minus.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the unary minus.
        /// </returns>
        public override int Evaluate()
        {
            return (- operand.Evaluate());
        }
    }
}
