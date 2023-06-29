using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Expenses.Common.Entities.Schemas
{
    [TableName("IncomeCategories")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [ExplicitColumns]
    public class IncomeCategorySchema
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
