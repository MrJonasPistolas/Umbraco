using Umbraco.Cms.Core.Models.PublishedContent;

namespace Blog.Common.Models
{
    public class SearchTopPostsViewModel : PublishedContentWrapped
    {
        public SearchTopPostsViewModel(
            IPublishedContent content, 
            IPublishedValueFallback publishedValueFallback
        ) : base(content, publishedValueFallback)
        {

        }

        public IEnumerable<IPublishedContent> SearchResults { get; set; } = Enumerable.Empty<IPublishedContent>();
        public bool HasSearched { get; set; }
    }
}
