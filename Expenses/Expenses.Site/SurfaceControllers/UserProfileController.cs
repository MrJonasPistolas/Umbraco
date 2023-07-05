using Expenses.Common.Models.Authentication.Requests;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Filters;
using Umbraco.Cms.Web.Website.Controllers;

namespace Expenses.Site.SurfaceControllers
{
    [UmbracoMemberAuthorize]
    public class UserProfileController : SurfaceController
    {
        #region > Properties <
        private readonly IMemberManager _memberManager;
        private readonly IMemberService _memberService;
        private readonly IMemberTypeService _memberTypeService;
        private readonly ICoreScopeProvider _scopeProvider;
        #endregion

        public UserProfileController(
            IUmbracoContextAccessor umbracoContextAccessor, 
            IUmbracoDatabaseFactory databaseFactory, 
            ServiceContext services, 
            AppCaches appCaches, 
            IProfilingLogger profilingLogger, 
            IPublishedUrlProvider publishedUrlProvider,
            IMemberManager memberManager,
            IMemberService memberService,
            IMemberTypeService memberTypeService,
            ICoreScopeProvider scopeProvider) 
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _memberManager = memberManager;
            _memberService = memberService;
            _memberTypeService = memberTypeService;
            _scopeProvider = scopeProvider;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateUmbracoFormRouteString]
        public async Task<IActionResult> HandleUpdateProfile([Bind(Prefix = "profileUpdateRequestModel")] ProfileUpdateRequestModel model)
        {
            if (ModelState.IsValid == false)
            {
                return CurrentUmbracoPage();
            }

            MergeRouteValuesToModel(model);

            MemberIdentityUser? currentMember = await _memberManager.GetUserAsync(HttpContext.User);
            if (currentMember == null!)
            {
                // this shouldn't happen, we also don't want to return an error so just redirect to where we came from
                return RedirectToCurrentUmbracoPage();
            }

            bool result = UpdateMember(model, currentMember);
            if (!result)
            {
                return CurrentUmbracoPage();
            }

            TempData["FormSuccess"] = true;

            // If there is a specified path to redirect to then use it.
            if (model.RedirectUrl.IsNullOrWhiteSpace() == false)
            {
                return Redirect(model.RedirectUrl!);
            }

            // Redirect to current page by default.
            return RedirectToCurrentUmbracoPage();
        }

        /// <summary>
        ///     We pass in values via encrypted route values so they cannot be tampered with and merge them into the model for use
        /// </summary>
        /// <param name="model"></param>
        private void MergeRouteValuesToModel(ProfileUpdateRequestModel model)
        {
            if (RouteData.Values.TryGetValue(nameof(ProfileUpdateRequestModel.RedirectUrl), out var redirectUrl) && redirectUrl != null)
            {
                model.RedirectUrl = redirectUrl.ToString();
            }
        }

        private bool UpdateMember(ProfileUpdateRequestModel model, MemberIdentityUser currentMember)
        {
            using ICoreScope scope = _scopeProvider.CreateCoreScope(autoComplete: true);

            IMember? member = _memberService.GetByKey(currentMember.Key);

            if (member == null)
            {
                // should never happen
                throw new InvalidOperationException($"Could not find a member with key: {member?.Key}.");
            }

            member.Properties["fullName"].SetValue(model.Fullname);
            member.Properties["gender"].SetValue(model.Gender);
            member.Properties["birthDate"].SetValue(model.BirthDate);
            member.Properties["address"].SetValue(model.Address);
            member.Properties["mobile"].SetValue(model.Mobile);

            _memberService.Save(member);

            return true;
        }
    }
}
