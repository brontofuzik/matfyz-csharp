using System;
using BenTools.Data;    // BinaryPriorityQueue

namespace CSharp_081020
{
    /// <summary>
    /// This (public) class represents a prefix tree.
    /// </summary>
    public class PrefixTree
    {
        #region Private Instance Fields

        /// <summary>
        /// The root node of this prefix tree.
        /// </summary>
        PrefixTreeNode rootNode;

        /// <summary>
        /// 
        /// </summary>
        PrefixTreeNode traceNode;

        #endregion // Private Instance Fields

        #region Public Instance Properties

        /// <summary>
        /// Gets the root node of this prefix tree,
        /// </summary>
        /// 
        /// <value>
        /// The root node of this prefix tree.
        /// </value>
        public PrefixTreeNode RootNode
        {
            get
            {
                return rootNode;
            }
        }

        #endregion // Public Instance Properties

        #region Public Instance Constructors

        /// <summary>
        /// Creates a prefix tree from a given character weights hash table.
        /// </summary>
        /// 
        /// <param name="symbolWeights">The character weights.</param>
        public PrefixTree(long[] symbolWeights)
        {
            // Create the (binary) priority queue of prefix tree nodes (leaves).
            BinaryPriorityQueue binaryPriorityQueue = new BinaryPriorityQueue();

            // Populate the (binary) priority queue of prefix tree nodes (leaves).
            for (int symbol = 0; symbol <= byte.MaxValue; symbol++)
            {
                if (symbolWeights[(byte)symbol] == 0)
                {
                    continue;
                }
                PrefixTreeNode prefixTreeNode = new PrefixTreeNode((byte)symbol, (long)symbolWeights[(byte)symbol]);
                binaryPriorityQueue.Push(prefixTreeNode);
            }

            // Build the prefix tree using a binary priority queue.
            while (binaryPriorityQueue.Count != 1)
            {
                // The node with the smallest weight becomes the right child of its parent.
                PrefixTreeNode rightChildNode = (PrefixTreeNode)binaryPriorityQueue.Pop();

                // The node with the second smallest weight becomes the left child of its parent.
                PrefixTreeNode leftChildNode = (PrefixTreeNode)binaryPriorityQueue.Pop();

                // Create the parent node and enqueue it.
                PrefixTreeNode parentNode = new PrefixTreeNode(leftChildNode, rightChildNode);
                binaryPriorityQueue.Push(parentNode);
            }

            // The last remaning node becomes the root node of the prefix tree.
            rootNode = (PrefixTreeNode)binaryPriorityQueue.Pop();

            // Recursively assign prefixes to the nodes strating from the root node.
            AssignPrefixes();
        }

        #endregion // Public Instance Constructors

        #region Public Instance Methods

        /// <summary>
        /// 
        /// </summary>
        public void AssignPrefixes()
        {
            rootNode.AssignPrefix("");
        }

        /// <summary>
        /// 
        /// </summary>
        public string[] GetSymbolCodes()
        {
            string[] symbolCodes = new string[byte.MaxValue + 1];

            rootNode.GetSymbolCode(symbolCodes);

            return symbolCodes;
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializeTrace()
        {
            traceNode = rootNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte Trace(int bit)
        {
            // Trace through the prefix tree.
            traceNode = (bit == 1) ? traceNode.LeftChildNode : traceNode.RightChildNode;

            byte symbol;
            if (traceNode.IsLeaf)
            {
                symbol = traceNode.Symbol;
                InitializeTrace();
            }
            else
            {
                symbol = 0;
            }
            return symbol;
        }

        #endregion // Public Instance Methods
    }
}
