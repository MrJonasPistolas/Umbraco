using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Expenses.Common.Entities.Schemas
{
    [TableName("Incomes")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [ExplicitColumns]
    public class IncomeSchema
    {
        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("MemberId")]
        public int MemberId { get; set; }

        [Column("IncomeCategoryId")]
        [ForeignKey(typeof(IncomeCategorySchema), Name = "FK_Incomes_IncomeCategories")]
        public int IncomeCategoryId { get; set; }

        [Column("Name")]
        [Length(120)]
        public string Name { get; set; }

        [Column("Amount")]
        public double Amount { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("Description")]
        [Length(255)]
        public string Description { get; set; }
    }
}
