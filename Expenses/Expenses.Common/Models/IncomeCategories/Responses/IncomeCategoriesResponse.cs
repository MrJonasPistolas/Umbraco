namespace Expenses.Common.Models.IncomeCategories.Responses
{
    public class IncomeCategoriesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MemberId { get; set; }
        public string Member { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
