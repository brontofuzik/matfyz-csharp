using System;

namespace CSharp_081124.Operators.UnaryOperators
{
    /// <summary>
    /// Public abstract class representing a unary operator.
    /// </summary>
    public abstract class UnaryOperator
        : Operator
    {
        /// <summary>
        /// The operand of the operator.
        /// </summary>
        protected Expression operand;

        /// <summary>
        /// Gets or sets the operand of the unary operator.
        /// </summary>
        /// 
        /// <value>
        /// The operand of the unary operator.
        /// </value>
        public Expression Operand
        {
            get
            {
                return operand;
            }
            set
            {
                operand = value;
            }
        }

        /// <summary>
        /// Creates a new unary operator with a given operand.
        /// </summary>
        /// 
        /// <param name="operand">The operand of the unary operator.</param>
        public UnaryOperator( Expression operand )
        {
            // Validate the operand argument.
            if (operand == null)
            {
                throw new ArgumentNullException();
            }

            // Initialize the instance fields.
            this.operand = operand;
        }
    }
}
