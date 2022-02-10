using System.ComponentModel;
using Bb.ComponentModel.DataAnnotations;

namespace Bb.Identity
{
    public interface IRegisterModel
    {

        [DisplayName("p:ILoginModel,k:UserName,l:en-us,d:UserName")]
        [Description("p:ILoginModel,k:UserNameDescription,l:en-us,d:UserName")]
        string UserName { get; set; }

        [DisplayName("p:ILoginModel,k:Email,l:en-us,d:Email")]
        [Description("p:ILoginModel,k:EmailDescription,l:en-us,d:Email")]
        string Email { get; set; }

        [Description("p:ILoginModel,k:PassordDescription,l:en-us,d:Password")]
        [PasswordPropertyText(true)]
        string Password { get; set; }

        [DisplayName("p:ILoginModel,k:Phone,l:en-us,d:Phone")]
        [Description("p:ILoginModel,k:PhoneDescription,l:en-us,d:Phone")]
        string Phone { get; set; }

    }

}
