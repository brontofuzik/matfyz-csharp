using System;
using System.IO; // Stream

namespace CSharp_081020
{
    /// <summary>
    /// This (public static) class represents a huffman algorithm.
    /// </summary>
    public static class Huffman
    {
        #region Public Static Methods

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="fileStreamIn"></param>
        /// <param name="fileStreamOut"></param>
        /// <param name="prefixTree"></param>
        public static void Compress(Stream fileStreamIn, Stream fileStreamOut, PrefixTree prefixTree)
        {
            // Get the symbol codes from the prefix tree.
            string[] symbolCodes = prefixTree.GetSymbolCodes();

            BitStream bitStream = new BitStream(fileStreamOut);

            int symbol;
            while ((symbol = fileStreamIn.ReadByte()) != -1)
            {
                string code = symbolCodes[symbol];
                foreach (char character in code)
                {
                    int bit = (character == '1') ? 1 : 0;
                    bitStream.WriteBit(bit);
                }
            }

            bitStream.Finish();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="fileStreamIn"></param>
        /// <param name="fileStreamOut"></param>
        /// <param name="prefixTree"></param>
        public static void Decompress(Stream fileStreamIn, Stream fileStreamOut, PrefixTree prefixTree)
        {
            BitStream bitStream = new BitStream(fileStreamIn);

            prefixTree.InitializeTrace();

            int bit;
            while ((bit = bitStream.ReadBit()) != -1)
            {
                byte symbol = prefixTree.Trace(bit);
                if (symbol != 0)
                {
                    fileStreamOut.WriteByte(symbol);
                }
            }
        }

        /// <summary>
        /// Counts the symbol weights in a given stream.
        /// </summary>
        /// 
        /// <param name="sr">The stream.</param>
        /// 
        /// <returns>
        /// The symbol weights in the given stream.
        /// </return>
        public static long[] CountSymbolWeights(Stream stream)
        {
            long[] symbolWeights = new long[byte.MaxValue + 1];

            int symbol;
            while ((symbol = stream.ReadByte()) != -1)
            {
                symbolWeights[symbol]++;
            }

            return symbolWeights;
        }

        #endregion // Public Static Methods
    }
}
