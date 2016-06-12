using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MineSweeper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("TODO");
                // TODO: Exit.
            }

            try
            {
                int width = Int32.Parse(args[0]);
                int height = Int32.Parse(args[1]);
                int mineCount = Int32.Parse(args[2]);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(width,height,mineCount));
            }
            catch (FormatException e)
            {
                Console.WriteLine("TODO");
                // TODO: Exit.
            }
        }
    }
}