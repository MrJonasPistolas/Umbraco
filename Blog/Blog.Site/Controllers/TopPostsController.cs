using Blog.Common.Interfaces;
using Blog.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Blog.Site.Controllers
{
    public class TopPostsController : RenderController
    {
        #region > Properties <
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly ISearchService _searchService;
        #endregion

        #region > Constructor <
        public TopPostsController(
            ILogger<RenderController> logger, 
            ICompositeViewEngine compositeViewEngine, 
            IUmbracoContextAccessor umbracoContextAccessor,
            IPublishedValueFallback publishedValueFallback,
            ISearchService searchService
        ) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _publishedValueFallback = publishedValueFallback;
            _searchService = searchService;
        }
        #endregion

        public override IActionResult Index()
        {
            // Get the queryString from the request
            string queryString = HttpContext.Request.Query["query"];

            // Create the view model and pass it to the view
            SearchTopPostsViewModel viewModel = new(CurrentPage!, _publishedValueFallback)
            {
                SearchResults = _searchService.SearchPosts(queryString),
                HasSearched = !string.IsNullOrEmpty(queryString),
            };

            return CurrentTemplate(viewModel);
        }
    }
}
