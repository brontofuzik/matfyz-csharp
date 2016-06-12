using System;
using System.IO;    // FileStream
using System.Text;  // StringBuilder

namespace CSharp_081020
{
    /// <summary>
    /// This class represents the application's entry point.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The application's entry point.
        /// </summary>
        /// 
        /// <param name="args">The commnad line arguments.</param>
        static void Main(string[] args)
        {
            FileStream fileStreamIn = null;
            FileStream fileStreamOut = null;

            try
            {
                // ===== Build the prefix tree ======

                // Open the input (uncompressed) file.
                fileStreamIn = new FileStream(args[0], FileMode.Open);

                // Count the character weights in the stream.
                long[] symbolWeights = Huffman.CountSymbolWeights(fileStreamIn);

                Console.WriteLine(ArrayToString(symbolWeights));

                // Build the prefix tree from the character weights.
                PrefixTree prefixTree = new PrefixTree(symbolWeights);

                // Close the input (uncompressed) file.
                fileStreamIn.Close();

                // ====== Compress file ======
                
                // Open the input (uncompressed) file.
                fileStreamIn = new FileStream(args[0], FileMode.Open);

                // Open the output (compressed) file.
                fileStreamOut = new FileStream(args[1], FileMode.Create);

                Huffman.Compress(fileStreamIn, fileStreamOut, prefixTree);

                // Close the input (uncompressed) file.
                fileStreamIn.Close();

                // Close the output (compressed) file.
                fileStreamOut.Close();

                // ===== Decompress the file =====

                // Open the input (compressed) file
                fileStreamIn = new FileStream(args[1], FileMode.Open);

                // Open the output (Uncompressed) file.
                fileStreamOut = new FileStream(args[2], FileMode.Create);

                Huffman.Decompress(fileStreamIn, fileStreamOut, prefixTree);
            }
            catch
            {
            }
            finally
            {
                // Close the input file.
                if (fileStreamIn != null)
                {
                    fileStreamIn.Close();
                }

                // Close the output file.
                if (fileStreamOut != null)
                {
                    fileStreamOut.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="array"></param>
        /// 
        /// <returns>
        /// 
        /// </returns>
        static string ArrayToString(object[] array)
        {
            StringBuilder arrayStringBuilder = new StringBuilder();

            arrayStringBuilder.Append("[");

            for (int arrayIndex = 0; arrayIndex < array.Length; arrayIndex++ )
            {
                arrayStringBuilder.Append(arrayIndex + ":" + array[arrayIndex] + ",");
            }

            if (array.Length > 0)
            {
                arrayStringBuilder.Remove(arrayStringBuilder.Length - 1, 1);
            }

            arrayStringBuilder.Append("]");

            return arrayStringBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="array"></param>
        /// 
        /// <returns>
        /// 
        /// </returns>
        static string ArrayToString(long[] array)
        {
            StringBuilder arrayStringBuilder = new StringBuilder();

            arrayStringBuilder.Append("[");

            for (int arrayIndex = 0; arrayIndex < array.Length; arrayIndex++)
            {
                arrayStringBuilder.Append(arrayIndex + ":" + array[arrayIndex] + ",");
            }

            if (array.Length > 0)
            {
                arrayStringBuilder.Remove(arrayStringBuilder.Length - 1, 1);
            }

            arrayStringBuilder.Append("]");

            return arrayStringBuilder.ToString();
        }
    }
}