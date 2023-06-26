using Umbraco.Cms.Core.Models.PublishedContent;

namespace Blog.Common.Interfaces
{
    public interface ISearchService
    {
        IEnumerable<IPublishedContent> SearchPosts(string query);
    }
}
