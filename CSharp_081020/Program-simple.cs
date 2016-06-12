#define ENABLE_TREE_VISUALIZATION
// #undef ENABLE_TREE_VISUALIZATION

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace HuffmanCoding
{
	class Node
	{
		public Node Left;
		public Node Right;
		public long Weight;
		public byte Symbol;		// Required only for leaf nodes and has no meaning in intermediate nodes.

		//
		// Parent correctly assigned during tree generation, however later it's not used anywhere
		// (neither during compression nor decompression). We hold this value only for debugging purposes.
		//
		public Node Parent;

		public Node(long weight) : this(weight, 0) { }

		public Node(long weight, byte symbol) {
			Weight = weight;
			Symbol = symbol;
		}
	}

	class Program
	{
		static Node CreateTree(byte[] symbols, long[] symbolCounts, out Node[] leafNodes) {
			leafNodes = new Node[symbols.Length];
			return CreateTree(symbols, symbolCounts, ref leafNodes, true);
		}

		static Node CreateTree(byte[] symbols, long[] symbolCounts) {
			Node[] leafNodes = null;
			return CreateTree(symbols, symbolCounts, ref leafNodes, false);
		}

		//
		// Don't be confused by generation of leafNodes array. I had an idea to use it during decompression,
		// however in the final version it probably won't be used at all.
		//
		static Node CreateTree(byte[] symbols, long[] symbolCounts, ref Node[] leafNodes, bool fillLeafNodes) {
			Queue<Node> symbolQueue = new Queue<Node>(symbols.Length);
			Queue<Node> innerNodeQueue = new Queue<Node>(symbols.Length);

			for (int i = 0; i < symbols.Length; i++) {
				Node symbolNode = new Node(symbolCounts[i], symbols[i]);
				symbolQueue.Enqueue(symbolNode);
				if (fillLeafNodes) {
					leafNodes[i] = symbolNode;
				}
			}

			while (symbolQueue.Count > 0 || innerNodeQueue.Count > 1) {
				Node min1, min2;

				//
				// Following two commented if statements represent the less optimal choice of minimal value nodes.
				// Uncommented lines tend to generate more balanced tree with while retaining all Huffman properties.
				//

				if (innerNodeQueue.Count == 0 || (symbolQueue.Count > 0 && symbolQueue.Peek().Weight <= innerNodeQueue.Peek().Weight)) {
				// if (innerNodeQueue.Count == 0 || (symbolQueue.Count > 0 && symbolQueue.Peek().Weight < innerNodeQueue.Peek().Weight)) {
					min1 = symbolQueue.Dequeue();
				} else {
					min1 = innerNodeQueue.Dequeue();
				}

				if (innerNodeQueue.Count == 0 || (symbolQueue.Count > 0 && symbolQueue.Peek().Weight <= innerNodeQueue.Peek().Weight)) {
				// if (innerNodeQueue.Count == 0 || (symbolQueue.Count > 0 && symbolQueue.Peek().Weight < innerNodeQueue.Peek().Weight)) {
					min2 = symbolQueue.Dequeue();
				} else {
					min2 = innerNodeQueue.Dequeue();
				}

				Node node = new Node(min1.Weight + min2.Weight);
				node.Left = min1;
				node.Right = min2;
				min1.Parent = node;
				min2.Parent = node;
				innerNodeQueue.Enqueue(node);
			}

			return innerNodeQueue.Dequeue();
		}

		static long[] CountSymbolsInStream(Stream s) {
			long[] counts = new long[256];

			int symbol;
			while ((symbol = s.ReadByte()) != -1) {
				counts[symbol]++;
			}

			return counts;
		}


		static void Main(string[] args) {
			
			FileStream fin = null;
			FileStream fout = null;

			try {
				fin = new FileStream(args[0], FileMode.Open);

				//
				// Create coding tree
				//

				long[] counts = CountSymbolsInStream(fin);

				int nonZeroCounts = 0;
				for (int i = 0; i < 256; i++) {
					if (counts[i] > 0) nonZeroCounts++;
				}

				long[] usedSymbolCounts = new long[nonZeroCounts];
				byte[] usedSymbols = new byte[nonZeroCounts];
				int used = 0;
				for (int i = 0; i < 256; i++) {
					if (counts[i] > 0) {
						usedSymbolCounts[used] = counts[i];
						usedSymbols[used] = (byte) i;
						used++;
					}
				}

				Array.Sort(usedSymbolCounts, usedSymbols);

				Node root = CreateTree(usedSymbols, usedSymbolCounts);

#if ENABLE_TREE_VISUALIZATION
				BinaryTreeVisualizer.DisplayTree(root);
#endif

                                // TODO - assignment

				//
				// Generate codes for each input symbol
				//

                                // Save symbol table (tree) into the file

                                // Save length of input
                                
                                // Save bits to output file

			} catch (IOException ex) {
				Console.WriteLine("Error: " + ex.Message);
			} finally {
				if (fout != null) fout.Close();
				if (fin != null) fin.Close();
			}
		}
	}
}
