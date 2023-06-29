using Expenses.Common.Models.Authentication.Requests;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Filters;
using Umbraco.Cms.Web.Common.Security;
using Umbraco.Cms.Web.Website.Controllers;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Expenses.Site.SurfaceControllers
{
    public class LoginController : SurfaceController
    {
        #region > Properties <
        private readonly IMemberSignInManager _signInManager;
        #endregion

        #region > Constructors <
        [ActivatorUtilitiesConstructor]
        public LoginController(
            IUmbracoContextAccessor umbracoContextAccessor, 
            IUmbracoDatabaseFactory databaseFactory, 
            ServiceContext services, 
            AppCaches appCaches, 
            IProfilingLogger profilingLogger, 
            IPublishedUrlProvider publishedUrlProvider,
            IMemberSignInManager signInManager) 
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _signInManager = signInManager;
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateUmbracoFormRouteString]
        public async Task<IActionResult> HandleLogin([Bind(Prefix = "loginModel")] LoginRequestModel model)
        {
            if (ModelState.IsValid == false)
            {
                return CurrentUmbracoPage();
            }

            MergeRouteValuesToModel(model);
            
            SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

            if (result.Succeeded)
            {
                TempData["LoginSuccess"] = true;

                // If there is a specified path to redirect to then use it.
                if (model.RedirectUrl.IsNullOrWhiteSpace() == false)
                {
                    // Validate the redirect URL.
                    // If it's not a local URL we'll redirect to the root of the current site.
                    return Redirect(Url.IsLocalUrl(model.RedirectUrl)
                        ? model.RedirectUrl
                        : CurrentPage!.AncestorOrSelf(1)!.Url(PublishedUrlProvider));
                }

                // Redirect to current URL by default.
                // This is different from the current 'page' because when using Public Access the current page
                // will be the login page, but the URL will be on the requested page so that's where we need
                // to redirect too.
                return RedirectToCurrentUmbracoUrl();
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("loginModel", "Member is locked out");
            }
            else if (result.IsNotAllowed)
            {
                ModelState.AddModelError("loginModel", "Member is not allowed");
            }
            else
            {
                ModelState.AddModelError("loginModel", "Invalid username or password");
            }

            return CurrentUmbracoPage();
        }

        /// <summary>
        /// We pass in values via encrypted route values so they cannot be tampered with and merge them into the model for use
        /// </summary>
        /// <param name="model"></param>
        private void MergeRouteValuesToModel(LoginRequestModel model)
        {
            if (RouteData.Values.TryGetValue(nameof(LoginRequestModel.RedirectUrl), out var redirectUrl) && redirectUrl != null)
            {
                model.RedirectUrl = redirectUrl.ToString();
            }
        }
    }
}
