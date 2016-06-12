using System;

namespace CSharp_081124.Operands
{
    /// <summary>
    /// Public abstract class representing a operand of an expression.
    /// </summary>
    public class Operand
        : Expression
    {
        /// <summary>
        /// The value of the operand.
        /// </summary>
        private int value;

        /// <summary>
        /// Gets or sets the value of the operand.
        /// </summary>
        /// 
        /// <value>
        /// The value of the operand.
        /// </value>
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Creates a new operand with a given value.
        /// </summary>
        /// 
        /// <param name="value">The value of the operand.</param>
        public Operand( int value )
        {
            this.value = value;
        }

        /// <summary>
        /// Evaluates the operand, i.e. returns its value.
        /// </summary>
        /// 
        /// <returns>The value of the operand.</returns>
        public override int Evaluate()
        {
            return value;
        }
    }
}
