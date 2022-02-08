using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Bb.IdentityIndividual.Pages
{

    public partial class Login
    {

        [Inject]
        public SignInManager<IdentityUser> SignInManager { get; set; }

        //[Inject]
        //public UserManager<IdentityUser> UserManager { get; set; }

        [Inject]
        public ClaimsPrincipal User { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }



    }
}
