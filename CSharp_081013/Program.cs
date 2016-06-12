using System;
using System.Collections.Generic;       // List
using System.IO;                        // StreamReader, StreamWriter

namespace CSharp_081013
{
    /// <summary>
    /// This class represents the application's entry point.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = null;
            StreamWriter sw = null;

            try
            {
                // Open the input file.
                sr = new StreamReader(args[0]);

                // Open the output file.
                sw = new StreamWriter(args[1]);

                // Get the block width.
                int blockWidth = Convert.ToInt32(args[2]);

                // Construct the word buffer using the given block width.
                WordBuffer wordBuffer = new WordBuffer(blockWidth);

                // WHY: Why cannot the string be declared within the while statement?
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line == "")
                    {
                        while (!wordBuffer.Empty)
                        {
                            line = wordBuffer.FlushLine();
                            sw.WriteLine(line);
                        }
                        sw.WriteLine();
                        continue;
                    }

                    wordBuffer.BufferLine(line);

                    while (wordBuffer.Flushable)
                    {
                        line = wordBuffer.FlushLine();
                        sw.WriteLine(line);
                    }
                }

                // Flush the word buffer if necessary.
                while (!wordBuffer.Empty)
                {
                    string line = wordBuffer.FlushLine();
                    sw.WriteLine(line);
                }
            }
            catch (ArgumentException e)
            {
                // Condition: blockWidth is less than or equal to zero.
                Console.WriteLine("error: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("error: " + e.Message);
            }
            finally
            {
                // Close the input file.
                if (sr != null)
                {
                    sr.Close();
                }

                // Close the output file.
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}