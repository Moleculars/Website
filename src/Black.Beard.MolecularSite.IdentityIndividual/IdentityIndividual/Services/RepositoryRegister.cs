using Bb.Identity;
using Microsoft.AspNetCore.Identity;

namespace Bb.IdentityIndividual.Services
{
    public class RepositoryRegister : IRepository<IRegisterModel>
    {

        public RepositoryRegister(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }


        public IRegisterModel GetNew()
        {
            return new RegisterModel()
            {
                 UserName = "User1"
            };
        }

        public async Task<bool> SaveNew(IRegisterModel item)
        {

            var hasher = new PasswordHasher<IdentityUser>(  );


            var identity = new IdentityUser()
            {
                UserName = item.UserName,
                Email = item.Email,
            };

            identity.PasswordHash = hasher.HashPassword(identity, item.Password);

            var result = await _userManager.CreateAsync(identity);

            return true;

        }


        private UserManager<IdentityUser> _userManager;

    }

}
