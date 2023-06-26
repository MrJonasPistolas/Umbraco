using Blog.Common.Interfaces;
using Examine;
using Examine.Search;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;

namespace Blog.Common.Services
{
    public class SearchService : ISearchService
    {
        #region > Properties <
        private readonly IExamineManager _examineManager;
        private readonly UmbracoHelper _umbracoHelper;
        #endregion

        #region > Constructor <
        public SearchService(IExamineManager examineManager, UmbracoHelper umbracoHelper)
        {
            _examineManager = examineManager;
            _umbracoHelper = umbracoHelper;
        }
        #endregion

        public IEnumerable<IPublishedContent> SearchPosts(string query)
        {
            IEnumerable<string> ids = Array.Empty<string>();
            if (!string.IsNullOrEmpty(query) && _examineManager.TryGetIndex("ExternalIndex", out IIndex? index))
            {
                List<SortableField> sortableFields = new List<SortableField>
                {
                    new SortableField("createDate")
                };

                ids = index.Searcher
                        .CreateQuery("content")
                        .NodeTypeAlias("contentPage")
                        .OrderByDescending(sortableFields.ToArray())
                        .Execute()
                        .Take(5)
                        .Select(x => x.Id);
            }

            foreach (var id in ids)
            {
                yield return _umbracoHelper.Content(id);
            }
        }
    }
}
