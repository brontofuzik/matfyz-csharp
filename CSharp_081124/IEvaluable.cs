using System;

namespace CSharp_081124
{
    /// <summary>
    /// Public interface representing an objects capability to be evaluated.
    /// </summary>
    interface IEvaluable
    {
        /// <summary>
        /// Evaluates the object.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the object.
        /// </returns>
        int Evaluate();
    }
}
