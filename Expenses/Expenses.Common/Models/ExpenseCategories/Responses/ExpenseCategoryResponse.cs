using Umbraco.Cms.Core.Models;

namespace Expenses.Common.Models.ExpenseCategories.Responses
{
    public class ExpenseCategoryResponse
    {
        public int Id { get; set; }
        public IMember Member { get; set; }
        public string Name { get; set; }
    }
}
