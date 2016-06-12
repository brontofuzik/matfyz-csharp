using System;

using CSharp_081124.Operators;

namespace CSharp_081124.Operators.BinaryOperators
{
    /// <summary>
    /// Public abstract class representing a binary operator.
    /// </summary>
    public abstract class BinaryOperator
        : Operator
    {
        /// <summary>
        /// The left operand of the binary operator.
        /// </summary>
        protected Expression leftOperand;

        /// <summary>
        /// The right operand of the binary operator.
        /// </summary>
        protected Expression rightOperand;

        /// <summary>
        /// Gets or sets the left operand of the binary operator.
        /// </summary>
        /// 
        /// <value>
        /// The left operand of the binary operator.
        /// </value>
        public Expression LeftOperand
        {
            get
            {
                return leftOperand;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                leftOperand = value;
            }
        }

        /// <summary>
        /// Gets or sets the right operand of the binary operator.
        /// </summary>
        /// 
        /// <value>
        /// The right operand of the binary operator.
        /// </value>
        public Expression RightOperand
        {
            get
            {
                return rightOperand;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                rightOperand = value;
            }
        }
        
        /// <summary>
        /// Evaluates the binary operator.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the bianry operator.
        /// </returns>
        public override int Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}
