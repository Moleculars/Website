using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Bb.IdentityIndividual.Pages
{

    public partial class Administration
    {

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        string ADMINISTRATION_ROLE = "Administrators";
        private System.Security.Claims.ClaimsPrincipal CurrentUser;

        [Inject]
        public UserManager<IdentityUser> _UserManager { get; set; }
        [Inject]
        public RoleManager<IdentityRole> _RoleManager { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }




        protected override async Task OnInitializedAsync()
        {

            //var UserName = httpContextAccessor.HttpContext.User.Identity.Name;

            // ensure there is a ADMINISTRATION_ROLE
            var RoleResult = await _RoleManager.FindByNameAsync(ADMINISTRATION_ROLE);
            if (RoleResult == null)
            {
                // Create ADMINISTRATION_ROLE Role
                await _RoleManager.CreateAsync(new IdentityRole(ADMINISTRATION_ROLE));
            }
            // Ensure a user named Admin@BlazorHelpWebsite.com is an Administrator
            var user = await _UserManager.FindByNameAsync("Admin@BlazorHelpWebsite.com");
            if (user != null)
            {
                // Is Admin@BlazorHelpWebsite.com in administrator role?
                var UserResult = await _UserManager.IsInRoleAsync(user, ADMINISTRATION_ROLE);
                if (!UserResult)
                {
                    // Put admin in Administrator role
                    await _UserManager.AddToRoleAsync(user, ADMINISTRATION_ROLE);
                }
            }
            // Get the current logged in user
            CurrentUser = (await AuthenticationStateTask).User;

        }


    }
}
