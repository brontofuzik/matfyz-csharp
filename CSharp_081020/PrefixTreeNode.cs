using System;

namespace CSharp_081020
{
    /// <summary>
    /// This (public) class represnents a prefix tree node.
    /// </summary>
    public class PrefixTreeNode
        : IComparable
    {
        #region Private Instance Fields

        /// <summary>
        /// 
        /// </summary>
        private readonly byte symbol;

        /// <summary>
        /// 
        /// </summary>
        private readonly long weight;

        /// <summary>
        /// 
        /// </summary>
        private string prefix;

        /// <summary>
        /// 
        /// </summary>
        private readonly PrefixTreeNode leftChildNode;

        /// <summary>
        /// 
        /// </summary>
        private readonly PrefixTreeNode rightChildNode;

        #endregion // Private Instance Fields

        #region Public Instance Properties

        /// <summary>
        /// 
        /// </summary>
        public byte Symbol
        {
            get
            {
                return symbol;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long Weight
        {
            get
            {
                return weight;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Prefix
        {
            get
            {
                return prefix;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PrefixTreeNode LeftChildNode
        {
            get
            {
                return leftChildNode;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PrefixTreeNode RightChildNode
        {
            get
            {
                return rightChildNode;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLeaf
        {
            get
            {
                return ((leftChildNode == null) && (rightChildNode == null)) ? true : false;
            }
        }

        #endregion // Internal Instance Properties

        #region Public Instance Contructors

        /// <summary>
        /// Creates a new prefix tree node using a given symbol, symbol's weight, its left child node and right child node. 
        /// </summary>
        /// 
        /// <param name="symbol">The given symbol.</param>
        /// <param name="weight">The symbol's weight.</param>
        /// <param name="leftChildNode">The left child node.</param>
        /// <param name="rightChildNode">The right child node.</param>
        public PrefixTreeNode(byte symbol, long weight, PrefixTreeNode leftChildNode, PrefixTreeNode rightChildNode)
        {
            // Initialize the instance fields.
            this.symbol = symbol;
            this.weight = weight;
            this.leftChildNode = leftChildNode;
            this.rightChildNode = rightChildNode;
        }

        /// <summary>
        /// Creates a new prefix tree node using a given symbol and symbol's weight. 
        /// </summary>
        /// 
        /// <param name="symbol">The given symbol.</param>
        /// <param name="weight">The symbol's weight.</param>
        internal PrefixTreeNode(byte symbol, long weight)
            : this(symbol, weight, null, null)
        {
        }

        /// <summary>
        /// Creates a new prefix tree node using its left child node and right child node. 
        /// </summary>
        /// 
        /// <param name="leftChildNode">The left child node.</param>
        /// <param name="rightChildNode">The right child node.</param>
        internal PrefixTreeNode(PrefixTreeNode leftChildNode, PrefixTreeNode rightChildNode)
            : this((byte)0, leftChildNode.Weight + rightChildNode.Weight, leftChildNode, rightChildNode)
        {
        }

        #endregion // Internal Instance Constructors

        #region Public Instance Methods

        /// <summary>
        /// Assigns a prefix to this node.
        /// </summary>
        ///
        /// <param name="prefix">The prefix to be assigned.</param>
        public void AssignPrefix(string prefix)
        {
            this.prefix = prefix;

            if (!IsLeaf)
            {
                leftChildNode.AssignPrefix(prefix + "1");
                rightChildNode.AssignPrefix(prefix + "0");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="symbolCodes"></param>
        /// 
        /// <returns></returns>
        public void GetSymbolCode(string[] symbolCodes)
        {
            if (IsLeaf)
            // The node is leaf => add it's prefix code to the hashtable.
            {
                symbolCodes[symbol] = prefix;
            }
            else
            // The node is not a leaf => recursively process it's children.
            {
                leftChildNode.GetSymbolCode(symbolCodes);
                rightChildNode.GetSymbolCode(symbolCodes);
            }
        }

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// 
        /// <param name="obj">An object to compare with this instance.</param>
        /// 
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the comparands. The return value has these meanings:
        /// <table>
        /// <tr><td>Less than zero</td><td>This instance is less than obj.</td></tr>
        /// <tr><td>Zero</td><td>This instance is equal to obj.</td></tr>
        /// <tr><td>Greater than zero</td><td>This instance is greater than obj.</td></tr>
        /// </table>
        /// </returns>
        /// 
        /// <exception cref="ArgumentException">
        /// Condition: <c>obj</c> is not the same type as this instance.
        /// </exception>
        public int CompareTo(object obj)
        {
            if (obj is PrefixTreeNode)
            {
                PrefixTreeNode otherPrefixTreeNode = (PrefixTreeNode)obj;
                return this.weight.CompareTo(otherPrefixTreeNode.weight);
            }
            else
            {
                throw new ArgumentException("Object is not a PrefixTreeNode");
            }

        }

        #endregion Public Instance Methods
    }
}
