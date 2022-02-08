using Bb.ComponentModel.Translations;
using Bb.CustomComponents;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using System.ComponentModel;
using System.Threading.Tasks;
using Bb.Translate;
using System.Linq;

namespace Bb.Pages
{

    public partial class Login
    {

        [Inject]
        public ITranslateService TranslateService { get; set; }
        public ServiceProvider ServiceProvider { get; set; }

        public LoginModel Model { get; set; }

        public ObjectDescriptor Descriptor { get; set; }

        protected override Task OnInitializedAsync()
        {

            var  o = (DescriptionAttribute)typeof(LoginModel).GetProperty("Login").GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();

            var p = o.GetLabel();

            Model = new LoginModel();
            Descriptor = new ObjectDescriptor(Model, Model.GetType(), TranslateService, ServiceProvider);

            return base.OnInitializedAsync();

        }

     
    }


    public class LoginModel
    {

        [Description("Login")]
        //[Description("p:LoginModel,k:Login,l:en-us,d:Login")]
        public string Login { get; set; }

        
        [Description("p:LoginModel,k:Passord,l:en-us,d:Password")]
        [PasswordPropertyText(true)]
        public string Password { get; set; }

    }

}
