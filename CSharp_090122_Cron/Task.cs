using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Task
    {
        #region Public instance fields

        public List<int> minutes = new List<int>();
        public List<int> hours = new List<int>();
        public List<int> daysOfMonth = new List<int>();
        public List<int> months = new List<int>();
        public List<int> daysOfWeek = new List<int>();
        public string command;

        #endregion // Public instance fields

        #region Public constructors

        public Task(string task)
        {
            for (int i = 0; i < 5; i++)
            {
                int indexOfFirstSpace = task.IndexOf(' ');
                string field = task.Substring(0, indexOfFirstSpace);
                task = task.Substring(indexOfFirstSpace).TrimStart(' ');

                List<int> list;
                switch (i)
                {
                    case 0: // minutes
                        list = minutes;
                        break;
                    case 1:
                        list = hours;
                        break;
                    case 2:
                        list = daysOfMonth;
                        break;
                    case 3:
                        list = months;
                        break;
                    case 4:
                        list = daysOfWeek;
                        break;
                    default:
                        list = null;
                        break;
                }

                if (field.Equals("*"))
                {
                    list.Add(-1);
                }
                else
                {
                    string[] subfields = field.Split(',');
                    foreach (string subfield in subfields)
                    {
                        if (subfield.IndexOf('-') == -1)
                        {
                            // Value.
                            int value = Int32.Parse(subfield);
                            list.Add(value);
                        }
                        else
                        {
                            // Interval.
                            string[] interval = subfield.Split('-');
                            int intervalFrom = Int32.Parse(interval[0]);
                            int intervalTo = Int32.Parse(interval[1]);
                            for (int value = intervalFrom; value <= intervalTo; value++)
                            {
                                list.Add(value);
                            }
                        }
                    }
                }
            }

            command = task;
        }

        #endregion // Public constructors

        #region Public instance methods

        public bool IsScheduledAt(DateTime dateTime)
        {
            // Minute.
            if (!minutes.Contains(-1))
            {
                if (!minutes.Contains(dateTime.Minute))
                {
                    return false;
                }
            }

            // Hour.
            if (!hours.Contains(-1))
            {
                if (!hours.Contains(dateTime.Hour))
                {
                    return false;
                }
            }

            // Day of month.
            if (!daysOfMonth.Contains(-1))
            {
                if (!daysOfMonth.Contains(dateTime.Day))
                {
                    return false;
                }
            }

            // Month.
            if (!months.Contains(-1))
            {
                if (!months.Contains(dateTime.Month))
                {
                    return false;
                }
            }

            // Day of week.
            if (!daysOfWeek.Contains(-1))
            {
                int dayOfWeek = (dateTime.DayOfWeek != DayOfWeek.Sunday) ? (int) dateTime.DayOfWeek : 7;

                if (!daysOfWeek.Contains(dayOfWeek))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion // Public instance methods
    }
}