using Expenses.Common.Core;
using Expenses.Common.Entities.Schemas;
using Expenses.Common.Models.ExpenseCategories.Responses;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Cms.Web.Common.Filters;

namespace Expenses.Site.Controllers
{
    public class ExpenseCategoryController : BaseApiController
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly IMemberService _memberService;

        public ExpenseCategoryController(IScopeProvider scopeProvider, IMemberService memberService)
        {
            _scopeProvider = scopeProvider;
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            using var scope = _scopeProvider.CreateScope();
            var queryResults = await scope.Database.FetchAsync<ExpenseCategorySchema>("SELECT * FROM ExpenseCategories");
            scope.Complete();
            List<ExpenseCategoryResponse> list = new List<ExpenseCategoryResponse>();
            foreach (var item in queryResults)
            {
                list.Add(new ExpenseCategoryResponse
                {
                    Id = item.Id,
                    Member = _memberService.GetById(item.MemberId),
                    Name = item.Name
                });
            }
            return HandleResult(new Result<IEnumerable<ExpenseCategoryResponse>>
            {
                Error = null,
                IsSuccess = true,
                Value = list
            });
        }

        [UmbracoMemberAuthorize("", "Administrators,Contributors", "")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ExpenseCategorySchema request)
        {
            using var scope = _scopeProvider.CreateScope();
            var result = await scope.Database.InsertAsync(request);
            scope.Complete();
            return HandleResult(new Result<bool>
            {
                Error = null,
                IsSuccess = true,
                Value = (int)result > -1
            });
        }

        [UmbracoMemberAuthorize("", "Administrators,Contributors", "")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(int id, ExpenseCategorySchema request)
        {
            request.Id = id;
            using var scope = _scopeProvider.CreateScope();
            var result = await scope.Database.UpdateAsync(request);
            scope.Complete();
            return HandleResult(new Result<bool>
            {
                Error = null,
                IsSuccess = true,
                Value = true
            });
        }

        [UmbracoMemberAuthorize("", "Administrators", "")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            using var scope = _scopeProvider.CreateScope();
            var result = await scope.Database.DeleteAsync(id);
            scope.Complete();
            return HandleResult(new Result<bool>
            {
                Error = null,
                IsSuccess = result > -1,
                Value = result > -1
            });
        }
    }
}
