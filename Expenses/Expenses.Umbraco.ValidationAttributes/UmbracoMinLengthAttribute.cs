using Expenses.Umbraco.ValidationAttributes.Helpers;
using Expenses.Umbraco.ValidationAttributes.Interfaces;
using Expenses.Umbraco.ValidationAttributes.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Umbraco.ValidationAttributes
{
    /// <summary>
    /// Specifies the minimum length of array or string data allowed in a property.
    /// </summary>
    public sealed class UmbracoMinLengthAttribute : MinLengthAttribute, IClientModelValidator, IUmbracoValidationAttribute
    {
        public string DictionaryKey { get; set; } = "MinLengthError";

        public UmbracoMinLengthAttribute(int length) : base(length) { }

        public UmbracoMinLengthAttribute(int length, string dictionaryKey) : base(length)
        {
            DictionaryKey = dictionaryKey;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            ErrorMessage = ValidationAttributesService.DictionaryValue(DictionaryKey);
            AttributeHelper.MergeAttribute(context.Attributes, "data-val", "true");
            AttributeHelper.MergeAttribute(context.Attributes, "data-val-minlength", ErrorMessage);
            AttributeHelper.MergeAttribute(context.Attributes, "data-val-minlength-min", Length.ToString());
        }
    }
}
