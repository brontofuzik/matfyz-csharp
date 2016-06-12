using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using (StreamReader sr = new StreamReader(args[0]))
                {
                    List<Task> tasks = new List<Task>();

                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Task task = new Task(line);
                        tasks.Add(task);
                    }

                    DateTime dateTimeFrom = new DateTime(2009, 1, 19, 0, 0, 0);
                    DateTime dateTimeTo = new DateTime(2009, 1, 26, 0, 0, 0);

                    for (DateTime dateTime = dateTimeFrom; dateTime <= dateTimeTo; dateTime = dateTime.AddMinutes(1))
                    {
                        foreach (Task task in tasks)
                        {
                            if (task.IsScheduledAt(dateTime))
                            {
                                Console.Write(DateTimeToString(dateTime));
                                Console.Write(" ");
                                Console.WriteLine(task.command);    
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        static string DateTimeToString(DateTime dateTime)
        {
            return String.Format("{0}-{1}-{2}-{3}-{4}", dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute);
        }
    }
}