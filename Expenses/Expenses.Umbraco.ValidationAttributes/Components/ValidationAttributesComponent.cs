using Expenses.Umbraco.ValidationAttributes.Services;
using Microsoft.Extensions.Configuration;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Web.Common;

namespace Expenses.Umbraco.ValidationAttributes.Components
{
    public class ValidationAttributesComposer : ComponentComposer<ValidationAttributesComponent> { }
    public class ValidationAttributesComponent : IComponent
    {
        public IUmbracoHelperAccessor _umbracoHelperAccessor;
        public IConfiguration _configuration;
        public ValidationAttributesComponent(
            IUmbracoHelperAccessor umbracoHelperAccessor,
            IConfiguration configuration
            )
        {
            _umbracoHelperAccessor = umbracoHelperAccessor;
            _configuration = configuration;
        }

        public void Initialize() => ValidationAttributesService.Start(_umbracoHelperAccessor, _configuration);
        public void Terminate() { }
    }
}
