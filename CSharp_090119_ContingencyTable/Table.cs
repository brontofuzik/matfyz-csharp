using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;

namespace CSharp_090119_ContingencyTable
{
    class Table
    {
        private class Row
        {
            private Table home;
            private string[] cells;

            public Row(Table home, string line)
            {
                this.home = home;
                cells = home.regex.Split(line);
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                foreach (string cell in cells)
                {
                    sb.Append(cell + ", ");
                }
                sb.Remove(sb.Length - 2, 2);
                return sb.ToString();
            }
        }

        private string[] columnNames;
        private List<Row> rows = new List<Row>();
        public Regex regex = new Regex(@" +");

        public Table(string line)
        {
            columnNames = regex.Split(line);
        }

        public Table(string[] columnNames)
        {
            this.columnNames = columnNames;
        }

        public void AddRow(string line)
        {
            Row row = new Row(this, line);
            rows.Add(row);
        }

        public Table CreateContingencyTable(string[] cathegories)
        {
            Table contingencyTable = new Table(cathegories);

            StringCollection[] stringCollections = new StringCollection[cathegories.Length - 1];
            for (int i = 0; i < stringCollections.Length; i++)
            {
                string columnName = cathegories[i];
                stringCollections[i] = GetValuesOfColumn(columnName);
            }

            return contingencyTable;
        }

        public StringCollection GetValuesOfColumn(string columnName)
        {
            return new StringCollection();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Row row in rows)
            {
                sb.Append(row + "\n");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}