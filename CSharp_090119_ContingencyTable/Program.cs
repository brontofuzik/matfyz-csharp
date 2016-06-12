using System;
using System.IO;

namespace CSharp_090119_ContingencyTable
{
    class Program
    {
        public static void Main(string[] args)
        {
            StreamReader sr = null;
            StreamWriter sw = null;
            try
            {
                sr = new StreamReader(args[0]);
                sw = new StreamWriter(args[1]);

                string line = sr.ReadLine();
                Table table = new Table(line);

                while ((line = sr.ReadLine()) != null)
                {
                    table.AddRow(line);
                }

                string[] cathegories = new string[args.Length - 2];
                for (int i = 0; i < args.Length - 2; i++)
                {
                    cathegories[i] = args[i + 2];
                }
                // [0] "mesic" [1] "typ" [2] "prodejce" [3] "trzba"
                Table contingencyTable = table.CreateContingencyTable(cathegories);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }

                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}