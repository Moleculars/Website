using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.UIComponents;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Bb.Services
{

    

    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(GuardMenuIdentity), LifeCycle = IocScopeEnum.Transiant)]
    public class GuardMenuIdentity : IGuardMenu
    {

        private IHttpContextAccessor _httpContext;

        public GuardMenuIdentity(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContext = httpContextAccessor;
        }

        public void Inititalize(GuardMenuProvider guardMenuProvider)
        {

        }

        public bool IsIdentified()
        {
            return _httpContext.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsIdentified(string role)
        {
            return _httpContext.HttpContext.User.IsInRole(role);
        }


    }

    //[ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(GuardMenuIdentity), LifeCycle = IocScopeEnum.Transiant)]
    //public class GuardMenuIdentity : IGuardMenu
    //{

    //    public GuardMenuIdentity(
    //        UserManager<IdentityUser> userManager,
    //        RoleManager<IdentityRole> roleManager,
    //        AuthenticationStateProvider authenticationStateProvider
    //        )
    //    {
    //        UserManager = userManager;
    //        RoleManager = roleManager;
    //        AuthenticationStateProvider = AuthenticationStateProvider;
    //    }

    //    public bool CanAccess()
    //    {
    //        return true;
    //    }



    //    public UserManager<IdentityUser> UserManager { get; set; }

    //    public RoleManager<IdentityRole> RoleManager { get; set; }

    //    public AuthenticationStateProvider AuthenticationStateProvider { get; set; }


    //    public async void Inititalize(GuardMenuProvider guardMenuProvider)
    //    {

    //        // ensure there is a ADMINISTRATION_ROLE
    //        var RoleResult = await RoleManager.FindByNameAsync(ADMINISTRATION_ROLE);
    //        if (RoleResult == null)
    //        {
    //            // Create ADMINISTRATION_ROLE Role
    //            await RoleManager.CreateAsync(new IdentityRole(ADMINISTRATION_ROLE));
    //        }
    //        // Ensure a user named Admin@BlazorHelpWebsite.com is an Administrator
    //        var user = await UserManager.FindByNameAsync("Admin@BlazorHelpWebsite.com");
    //        if (user != null)
    //        {
    //            // Is Admin@BlazorHelpWebsite.com in administrator role?
    //            var UserResult = await UserManager.IsInRoleAsync(user, ADMINISTRATION_ROLE);
    //            if (!UserResult)
    //            {
    //                // Put admin in Administrator role
    //                await UserManager.AddToRoleAsync(user, ADMINISTRATION_ROLE);
    //            }
    //        }
    //        // Get the current logged in user
    //        CurrentUser = (await AuthenticationStateTask).User;

    //    }

    //    private string ADMINISTRATION_ROLE = "Administrators";
    //    private System.Security.Claims.ClaimsPrincipal CurrentUser;
    //    private Task<AuthenticationState> AuthenticationStateTask;

    //}

}
