using System;
using System.Collections.Generic;

using CSharp_081124.Operands;
using CSharp_081124.Operators.BinaryOperators;

namespace CSharp_081124
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="args"></param>
        static void Main( string[] args )
        {
            Expression expression = buildExpressionFromPrefixNotation( args );

            Console.WriteLine( expression.Evaluate() );

            Console.ReadKey();
        }

        /// <summary>
        /// Builds an expression from its prefix notation.
        /// </summary>
        /// 
        /// <param name="prefixNotation">The prefix notation of the expression.</param>
        /// 
        /// <returns>
        /// The abstract syntax tree representing the expression.
        /// </returns>
        static Expression buildExpressionFromPrefixNotation( string[] prefixNotation )
        {
            // The stack is used to build the abstract syntax tree of the expression.
            Stack<Expression> stack = new Stack<Expression>();
            Expression expression = null;

            // Process all tokens of the expression.
            foreach (String token in prefixNotation)
            {
                // Determine and create the expression.
                switch (token)
                {
                    case "+":

                        expression = new Plus();
                        break;

                    case "-":

                        expression = new Minus();
                        break;

                    case "*":

                        expression = new Multiplication();
                        break;

                    case "/":

                        expression = new Division();
                        break;
                        
                    default:
                    
                        int value = Int32.Parse( token );
                        expression = new Operand( value );
                        break;
                }

                // Push the expression onto the stack.
                stack.Push( expression );

                if (expression is Operand)
                {
                    processStack( stack );
                }
            }

            // Return the top of the stack.
            return stack.Peek();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="stack"></param>
        static void processStack( Stack<Expression> stack )
        {
            if (stack.Count == 1)
            {
                return;
            }

            Expression poppedExpression = stack.Pop();

            BinaryOperator topBinaryOperator = (stack.Peek() as BinaryOperator);

            if (topBinaryOperator.LeftOperand == null)
            {
                topBinaryOperator.LeftOperand = poppedExpression;
            }
            else
            {
                topBinaryOperator.RightOperand = poppedExpression;
                processStack( stack );
            }
        }
    }
}
