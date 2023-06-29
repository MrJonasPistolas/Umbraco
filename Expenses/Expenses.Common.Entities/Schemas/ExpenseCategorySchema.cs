using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Expenses.Common.Entities.Schemas
{
    [TableName("ExpenseCategories")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [ExplicitColumns]
    public class ExpenseCategorySchema
    {
        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("MemberId")]
        public int MemberId { get; set; }

        [Column("Name")]
        [Length(120)]
        public string Name { get; set; }
    }
}
