using Expenses.Common.DataTables;
using Expenses.Common.Entities.Schemas;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Cms.Web.Common.Filters;

namespace Expenses.Site.Controllers
{
    [UmbracoMemberAuthorize]
    public class IncomeCategoryController : BaseApiController
    {
        #region > Properties <
        private readonly IScopeProvider _scopeProvider;
        private readonly IMemberService _memberService;
        #endregion

        #region > Constructors <
        public IncomeCategoryController(
            IScopeProvider scopeProvider,
            IMemberService memberService)
        {
            _scopeProvider = scopeProvider;
            _memberService = memberService;
        }
        #endregion

        #region > Methods <
        [HttpPost]
        [Route("Table")]
        public async Task<IActionResult> LoadTabledAsync([FromBody] DataTablesParameters param)
        {
            var draw = param.draw;
            var row = param.start;
            var rowPerPage = param.length;
            var columnIndex = param.order[0].column;
            var columnName = param.columns[columnIndex].data;
            var columnSortOrder = param.order[0].dir;
            var searchValue = param.search.value;

            using var scope = _scopeProvider.CreateScope();
            
            #region Search
            string searchQuery = string.Empty;
            if (param.search.value != "") 
            {
                searchQuery = $" AND (Name LIKE '%{searchValue}%')";
            }
            #endregion

            #region Total number of records without filtering
            var queryTotalRecordsResults = await scope.Database.FetchAsync<IncomeCategorySchema>("SELECT * FROM IncomeCategories");
            var totalRecords = queryTotalRecordsResults.Count;
            #endregion

            #region Total number of records with filtering
            var queryTotalRecordsWithFilterResults = await scope.Database.FetchAsync<IncomeCategorySchema>($"SELECT * FROM IncomeCategories WHERE 1 {searchQuery}");
            var totalRecordsWithFilter = queryTotalRecordsWithFilterResults.Count;
            #endregion

            #region Fetch the Data
            var queryResults = await scope.Database.FetchAsync<IncomeCategorySchema>($"SELECT * FROM IncomeCategories WHERE 1 {searchQuery} ORDER BY {columnName} {columnSortOrder} LIMIT {rowPerPage} OFFSET {row}");
            scope.Complete();
            #endregion

            dynamic response = new
            {
                Data = queryResults,
                Draw = draw.ToString(),
                RecordsFiltered = totalRecordsWithFilter,
                RecordsTotal = totalRecords,
            };

            return Ok(response);
        }
        #endregion
    }
}
