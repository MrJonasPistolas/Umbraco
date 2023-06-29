namespace Expenses.Common.Models.ExpenseCategories.Requests
{
    public class ExpenseCategoryRequest
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; }
    }
}
