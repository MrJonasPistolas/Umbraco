namespace Expenses.Common.DataTables
{
    public class DataTablesParameters
    {
        public int draw { get; set; }

        public DataTablesColumn[] columns { get; set; }

        public DataTablesOrder[] order { get; set; }

        public int start { get; set; }

        public int length { get; set; }

        public DataTablesSearch search { get; set; }

        public string SortOrder
        {
            get
            {
                if (columns == null || order == null || order.Length == 0)
                {
                    return null;
                }

                return columns[order[0].column].data + ((order[0].dir == "desc") ? (" " + order[0].dir) : string.Empty);
            }
        }

        public IEnumerable<string> AdditionalValues { get; set; }
    }
}
