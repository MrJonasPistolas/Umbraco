namespace Expenses.Common.DataTables
{
    public class DataTablesColumn
    {
        public string data { get; set; }

        public string name { get; set; }

        public bool searchable { get; set; }

        public bool orderable { get; set; }

        public DataTablesSearch search { get; set; }
    }
}
