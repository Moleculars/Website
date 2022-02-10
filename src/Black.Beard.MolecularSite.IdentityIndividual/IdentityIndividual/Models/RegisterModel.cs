using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bb.Identity
{

    public class RegisterModel : IRegisterModel
    {

        [DisplayName("p:RegisterModel,k:UserName,l:en-us,d:UserName")]
        [Description("p:RegisterModel,k:UserNameDescription,l:en-us,d:UserName")]
        [Required]
        public string UserName { get; set; }

        [DisplayName("p:RegisterModel,k:Email,l:en-us,d:Email")]
        [Description("p:RegisterModel,k:EmailDescription,l:en-us,d:Email")]
        [Required]
        [EmailAddress(ErrorMessage = "p:ILoginModel,k:ValidatationEmail,l:en-us,d:Invalid email address")]
        [PersonalData] 
        public string Email { get; set; }

        [Description("p:RegisterModel,k:PassordDescription,l:en-us,d:Password")]
        [PasswordPropertyText(true)]
        [Required]
        public string Password { get; set; }

        [DisplayName("p:RegisterModel,k:Phone,l:en-us,d:Phone")]
        [Description("p:RegisterModel,k:PhoneDescription,l:en-us,d:Phone")]
        [PersonalData] 
        public string Phone { get; set; }

    }

}
