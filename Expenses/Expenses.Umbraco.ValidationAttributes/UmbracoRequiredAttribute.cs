using Expenses.Umbraco.ValidationAttributes.Helpers;
using Expenses.Umbraco.ValidationAttributes.Interfaces;
using Expenses.Umbraco.ValidationAttributes.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Umbraco.ValidationAttributes
{
    /// <summary>
    /// Specifies that a data field value is required.
    /// </summary>
    public sealed class UmbracoRequiredAttribute : RequiredAttribute, IClientModelValidator, IUmbracoValidationAttribute
    {
        public string DictionaryKey { get; set; } = "RequiredError";

        public UmbracoRequiredAttribute() : base() { }

        public UmbracoRequiredAttribute(string dictionaryKey) : base()
        {
            DictionaryKey = dictionaryKey;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            ErrorMessage = ValidationAttributesService.DictionaryValue(DictionaryKey);
            AttributeHelper.MergeAttribute(context.Attributes, "data-val-required", ErrorMessage);
        }
    }
}
