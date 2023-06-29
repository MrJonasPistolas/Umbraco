using Expenses.Umbraco.ValidationAttributes.Helpers;
using Expenses.Umbraco.ValidationAttributes.Interfaces;
using Expenses.Umbraco.ValidationAttributes.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Umbraco.ValidationAttributes
{
    /// <summary>
    /// Specifies that a data field value mus be a valid Email Address
    /// </summary>
    public class UmbracoEmailAddressAttribute : RegularExpressionAttribute, IClientModelValidator, IUmbracoValidationAttribute
    {
        public string DictionaryKey { get; set; } = "EmailError";

        private static new string Pattern { get; set; } = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public UmbracoEmailAddressAttribute() : base(Pattern) { }

        public UmbracoEmailAddressAttribute(string dictionaryKey) : base(Pattern)
        {
            DictionaryKey = dictionaryKey;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            ErrorMessage = ValidationAttributesService.DictionaryValue(DictionaryKey);

            string customConfig = ValidationAttributesService.GetConfigKey(Constants.Configuration.EmailInputType);
            if (!string.IsNullOrEmpty(customConfig))
            {
                AttributeHelper.MergeAttribute(context.Attributes, "type", customConfig.ToLower(), replaceExisting: true);
            }
            else
            {
                AttributeHelper.MergeAttribute(context.Attributes, "type", "email", replaceExisting: true);
            }

            AttributeHelper.MergeAttribute(context.Attributes, "data-val", "true");
            AttributeHelper.MergeAttribute(context.Attributes, "data-val-email", ErrorMessage);
        }
    }
}
