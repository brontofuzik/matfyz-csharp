using System;
using System.Collections.Generic;

namespace CSharp_081215
{
    /// <summary>
    /// 
    /// </summary>
    abstract class Expression
    {
        #region Public static methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exprString"></param>
        /// <returns></returns>
		public static Expression ParsePrefixExpression( string exprString )
        {
			string[] tokens = exprString.Split(' ');

			Stack<OperatorExpression> unresolved = new Stack<OperatorExpression>();
			foreach (string token in tokens) {
				switch (token) {
				case "+":
					unresolved.Push(new PlusExpression());
					break;

				case "-":
					unresolved.Push(new MinusExpression());
					break;

				case "*":
					unresolved.Push(new MultiplyExpression());
					break;

				case "/":
					unresolved.Push(new DivideExpression());
					break;

				default:
					int value;
					if (!int.TryParse(token, out value)) {
						throw new FormatException(string.Format("\"{0}\" is not a valid integer value.", token));
					}

					Expression expr = new ValueExpression(value);
					while (unresolved.Count > 0) {
						OperatorExpression oper = unresolved.Peek();
						if (oper.AddOperand(expr)) {
							unresolved.Pop();
							expr = oper;
						} else {
							expr = null;
							break;
						}
					}

					if (expr != null) return expr;
					break;
				}
			}

			throw new FormatException("At least one operator is missing some of its operands.");
        }

        #endregion // Public static methods

        #region Public instance methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract int Evaluate();

        /// <summary>
        /// Accepts an abstract visitor.
        /// [DP: Visitor]
        /// </summary>
        public abstract T Accept< T >( IVisitor< T > visitor );
        
        #endregion // Public instance methods

    }

    /// <summary>
    /// 
    /// </summary>
	class ValueExpression
        : Expression
    {
		protected int value;

		public ValueExpression()
        {
        }

		public ValueExpression( int value )
        {
			this.value = value;
		}

		public int Value
        {
			get { return this.value; }
			set { this.value = value; }
        }

        #region Public instance methods

        public override int Evaluate()
        {
			return value;
        }

        public override T Accept< T >( IVisitor< T > visitor )
        {
            return visitor.VisitValueExpression( this );
        }

        #endregion // Public instance methods
    }

    /// <summary>
    /// 
    /// </summary>
	abstract class OperatorExpression
        : Expression
    {
		public abstract bool AddOperand( Expression op );
	}
    
    /// <summary>
    /// 
    /// </summary>
	abstract class UnaryExpression
        : OperatorExpression
    {
		protected Expression op;

		public Expression Op
        {
			get { return op; }
			set { op = value; }
		}

		public override bool AddOperand( Expression op )
        {
			if (this.op == null)
            {
				this.op = op;
			}
			return true;
		}

		public sealed override int Evaluate()
        {
			return Evaluate(op.Evaluate());
		}

		protected abstract int Evaluate(int opValue);
	}

    /// <summary>
    /// 
    /// </summary>
	abstract class BinaryExpression
        : OperatorExpression
    {
		protected Expression op0, op1;

		public Expression Op0
        {
			get { return op0; }
			set { op0 = value; }
		}

		public Expression Op1
        {
			get { return op1; }
			set { op1 = value; }
		}

		public override bool AddOperand( Expression op )
        {
			if (op0 == null)
            {
				op0 = op;
				return false;
			}
            else if (op1 == null)
            {
				op1 = op;
			}
			return true;
		}

		public sealed override int Evaluate()
        {
			return Evaluate( op0.Evaluate(), op1.Evaluate() );
		}

		protected abstract int Evaluate( int op0Value, int op1Value );
	}

    /// <summary>
    /// 
    /// </summary>
	class PlusExpression
        : BinaryExpression
    {
        #region Public instance methods

        protected override int Evaluate( int op0Value, int op1Value )
        {
			return op0Value + op1Value;
        }

        public override T Accept< T >( IVisitor< T > visitor )
        {
            return visitor.VisitPlusExpression( this );
        }

        #endregion // Public insatance methods
    }

    /// <summary>
    /// 
    /// </summary>
	class MinusExpression
        : BinaryExpression
    {
        #region Public instance methods

        protected override int Evaluate( int op0Value, int op1Value )
        {
			return op0Value - op1Value;
		}

        public override T Accept< T >( IVisitor< T > visitor )
        {
            return visitor.VisitMinusExpression( this );
        }

        #endregion // Public insatance methods
	}

    /// <summary>
    /// 
    /// </summary>
	class MultiplyExpression
        : BinaryExpression
    {
        #region Public instance methods
        
        protected override int Evaluate( int op0Value, int op1Value )
        {
			return op0Value * op1Value;
		}

        public override T Accept< T >( IVisitor< T > visitor )
        {
            return visitor.VisitMultiplyExpression( this );
        }

        #endregion // Public insatance methods
	}

    /// <summary>
    /// 
    /// </summary>
	class DivideExpression
        : BinaryExpression
    {
        #region Public instance methods

        protected override int Evaluate( int op0Value, int op1Value )
        {
			return op0Value / op1Value;
		}

        public override T Accept< T >( IVisitor< T > visitor )
        {
            return visitor.VisitDivideExpression( this );
        }

        #endregion // Public insatance methods
	}

    /// <summary>
    /// An abstract visitor.
    /// [DP: Visitor]
    /// </summary>
    interface IVisitor< T >
    {
        /// <summary>
        /// Visits a value expression.
        /// </summary>
        /// <param name="valueExpression"></param>
        T VisitValueExpression( ValueExpression valueExpression );

        /// <summary>
        /// Visits a plus expression.
        /// </summary>
        /// <param name="plusExpression"></param>
        T VisitPlusExpression( PlusExpression plusExpression );

        /// <summary>
        /// Visits a minus expression.
        /// </summary>
        /// <param name="minusExpression"></param>
        T VisitMinusExpression( MinusExpression minusExpression );

        /// <summary>
        /// Visits a multiply expression.
        /// </summary>
        /// <param name="multiplyExpression"></param>
        T VisitMultiplyExpression( MultiplyExpression multiplyExpression );

        /// <summary>
        /// Visits a divide expression.
        /// </summary>
        /// <param name="divideExpression"></param>
        T VisitDivideExpression( DivideExpression divideExpression );
    }

    /// <summary>
    /// A concrete (double) visitor.
    /// </summary>
    class DoubleVisitor
        : IVisitor< double >
    {
        /// <summary>
        /// Visits a value expression.
        /// </summary>
        /// <param name="valueExpression"></param>
        public double VisitValueExpression( ValueExpression valueExpression )
        {
            return (double)valueExpression.Evaluate();
        }

        /// <summary>
        /// Visits a plus expression.
        /// </summary>
        /// <param name="plusExpression"></param>
        public double VisitPlusExpression( PlusExpression plusExpression )
        {
            double op0 = plusExpression.Op0.Evaluate();
            double op1 = plusExpression.Op1.Evaluate();
            return op0 + op1;
        }

        /// <summary>
        /// Visits a minus expression.
        /// </summary>
        /// <param name="minusExpression"></param>
        public double VisitMinusExpression( MinusExpression minusExpression )
        {
            double op0 = minusExpression.Op0.Evaluate();
            double op1 = minusExpression.Op1.Evaluate();
            return op0 - op1;
        }

        /// <summary>
        /// Visits a multiply expression.
        /// </summary>
        /// <param name="multiplyExpression"></param>
        public double VisitMultiplyExpression( MultiplyExpression multiplyExpression )
        {
            double op0 = multiplyExpression.Op0.Evaluate();
            double op1 = multiplyExpression.Op1.Evaluate();
            return op0 * op1;
        }

        /// <summary>
        /// Visits a divide expression.
        /// </summary>
        /// <param name="divideExpression"></param>
        public double VisitDivideExpression( DivideExpression divideExpression )
        {
            double op0 = divideExpression.Op0.Evaluate();
            double op1 = divideExpression.Op1.Evaluate();
            return op0 / op1;
        }
    }

    /// <summary>
    /// A concrete (infix) visitor.
    /// </summary>
    class InfixVisitor
        : IVisitor< string >
    {
        /// <summary>
        /// Visits a value expression.
        /// </summary>
        /// <param name="valueExpression"></param>
        public string VisitValueExpression( ValueExpression valueExpression )
        {
            return "";
        }

        /// <summary>
        /// Visits a plus expression.
        /// </summary>
        /// <param name="plusExpression"></param>
        public string VisitPlusExpression( PlusExpression plusExpression )
        {
            return "";
        }

        /// <summary>
        /// Visits a minus expression.
        /// </summary>
        /// <param name="minusExpression"></param>
        public string VisitMinusExpression( MinusExpression minusExpression )
        {
            return "";
        }

        /// <summary>
        /// Visits a multiply expression.
        /// </summary>
        /// <param name="multiplyExpression"></param>
        public string VisitMultiplyExpression( MultiplyExpression multiplyExpression )
        {
            return "";
        }

        /// <summary>
        /// Visits a divide expression.
        /// </summary>
        /// <param name="divideExpression"></param>
        public string VisitDivideExpression( DivideExpression divideExpression )
        {
            return "";
        }
    }
	
    /// <summary>
    /// An entry point of the application.
    /// </summary>
	class Program
    {
		static void Main( string[] args )
        {
            //Expression expr = Expression.ParsePrefixExpression( "+" );
            //Expression expr = Expression.ParsePrefixExpression( "+ 1" );
            //Expression expr = Expression.ParsePrefixExpression( "42" );
            //Expression expr = Expression.ParsePrefixExpression( "+ 1 2" );
			Expression expr = Expression.ParsePrefixExpression( "/ + - 5 2 * 2 + 3 3 2" );

			Console.WriteLine( "Expression value in int: {0}", expr.Evaluate() );

            DoubleVisitor doubleVisitor = new DoubleVisitor();
            Console.WriteLine( "Expression value in double: {0}", expr.Accept( doubleVisitor ) );
		}
	}
}