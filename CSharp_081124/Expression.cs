using System;

namespace CSharp_081124
{
    /// <summary>
    /// Public abstract class representing an expression.
    /// </summary>
    public abstract class Expression
        : IEvaluable
    {
        /// <summary>
        /// Evaluates the expression.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the expression.
        /// </returns>
        public abstract int Evaluate();
    }
}
