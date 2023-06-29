using Expenses.Umbraco.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Common.Models.Authentication.Requests
{
    public class LoginRequestModel
    {
        [UmbracoDisplayName("LoginPage.DisplayNames.Email")]
        [UmbracoRequired(DictionaryKey = "LoginPage.Errors.Email.Required")]
        [UmbracoEmailAddress(DictionaryKey = "LoginPage.Errors.Email.Wrong")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [UmbracoDisplayName("LoginPage.DisplayNames.Password")]
        [UmbracoRequired(DictionaryKey = "LoginPage.Errors.Password.Required")]
        [DataType(DataType.Password)]
        [UmbracoStringLength(25, DictionaryKey = "LoginPage.Errors.Password.StringLength")]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }

        public string? RedirectUrl { get; set; }
    }
}
