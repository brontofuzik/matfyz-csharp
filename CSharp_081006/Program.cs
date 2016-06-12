using System;
using System.Collections.Generic;   // List
using System.IO;                    // StreamReader, StreamWriter
using System.Text;                  // StringBuilder

namespace CSharp_081006
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

                // Get the maximum width.
                int maxWidth = Convert.ToInt32(args[2]);

                // Get the delimiters.
                char[] delimiters = new char[] { ' ', '\t' };

                string line;
                int currentWidth = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    // Trim the line.
                    line = line.Trim();
                    
                    // Line was empty => break the line.
                    if (line.Length == 0)
                    {
                        sw.WriteLine();
                        currentWidth = 0;
                        continue;
                    }

                    // Split the line into words.
                    string[]  words = line.Split(delimiters);

                    // Write the words to the stream.
                    foreach (string word in words)
                    {
                        // If the next word would exceed the maximum width,
                        // write it on the next line.
                        if ((currentWidth != 0) && ((currentWidth + 1 + word.Length) > maxWidth))
                        {
                            sw.WriteLine();
                            currentWidth = 0;
                        }

                        // Write a space if not at the begining of a line.
                        if (currentWidth != 0)
                        {
                            sw.Write(" ");
                            currentWidth++;
                        }

                        // Write the word to the stream.
                        sw.Write(word);
                        currentWidth += word.Length;
                    }

                    // The stream is not over => break the line.
                    if (!sr.EndOfStream)
                    {
                        sw.WriteLine();
                        currentWidth = 0;
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch (System.Security.SecurityException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch (ArgumentException e)
            {
                Console.Error.WriteLine("Error: " + e.Message);
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