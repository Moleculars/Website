using System.ComponentModel;
using Bb.ComponentModel.DataAnnotations;

namespace Bb.Identity
{

    public class LoginModel : ILogin
    {

        public LoginModel() 
        {

        }

        [Description("Login")]
        //[Description("p:LoginModel,k:Login,l:en-us,d:Login")]
        public string Login { get; set; }

        
        [Description("p:LoginModel,k:Passord,l:en-us,d:Password")]
        [PasswordPropertyText(true)]
        public string Password { get; set; }

    }

}
