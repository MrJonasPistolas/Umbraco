using Expenses.Umbraco.ValidationAttributes.Interfaces;
using Expenses.Umbraco.ValidationAttributes.Services;
using System.ComponentModel;

namespace Expenses.Umbraco.ValidationAttributes
{
    public sealed class UmbracoDisplayNameAttribute : DisplayNameAttribute, IUmbracoValidationAttribute
    {
        public string DictionaryKey { get; set; }

        public UmbracoDisplayNameAttribute(string dictionaryKey) : base()
        {
            DictionaryKey = dictionaryKey;
        }

        public override string DisplayName => ValidationAttributesService.DictionaryValue(DictionaryKey);
    }
}
