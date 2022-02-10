using Bb.ComponentModel.DataAnnotations;
using Bb.Identity;
using Microsoft.AspNetCore.Identity;

namespace Bb.IdentityIndividual.Services
{

    public class RepositoryLogin : IRepository<IRegisterModel>
    {

        public RepositoryLogin(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }


        public IRegisterModel GetNew()
        {
            return new RegisterModel();
        }


        private SignInManager<IdentityUser> _signInManager;


    }

}
