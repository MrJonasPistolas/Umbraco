using Expenses.Common.Entities.Schemas;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Infrastructure.Migrations;

namespace Expenses.Data.Migrations
{
    public class AddInitialTables : MigrationBase
    {
        public AddInitialTables(IMigrationContext context) : base(context) { }

        protected override void Migrate()
        {
            Logger.LogDebug("Running migration {MigrationStep}", "AddInitialTables");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("ExpenseCategories") == false)
            {
                Create.Table<ExpenseCategorySchema>().Do();
            }
            else
            {
                Logger.LogDebug("The database table {DbTable} already exists, skipping", "ExpenseCategories");
            }

            if (TableExists("Expenses") == false)
            {
                Create.Table<ExpenseSchema>().Do();
            }
            else
            {
                Logger.LogDebug("The database table {DbTable} already exists, skipping", "Expenses");
            }

            if (TableExists("IncomeCategories") == false)
            {
                Create.Table<IncomeCategorySchema>().Do();
            }
            else
            {
                Logger.LogDebug("The database table {DbTable} already exists, skipping", "IncomeCategories");
            }

            if (TableExists("Incomes") == false)
            {
                Create.Table<IncomeSchema>().Do();
            }
            else
            {
                Logger.LogDebug("The database table {DbTable} already exists, skipping", "Incomes");
            }
        }
    }
}
