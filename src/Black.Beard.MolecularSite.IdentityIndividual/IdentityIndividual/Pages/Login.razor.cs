using Bb.ComponentModel.Translations;
using Bb.CustomComponents;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using System.ComponentModel;
using System.Threading.Tasks;
using Bb.Translate;
using System.Linq;
using Bb.ComponentModel.DataAnnotations;
using Bb.Identity;

namespace Bb.IdentityIndividual.Pages
{

    public partial class Login
    {

        [Inject]
        public ITranslateService TranslateService { get; set; }

        [Inject]
        public IServiceProvider ServiceProvider { get; set; }

        [Inject]
        public IRepository<ILogin> Repository { get; set; }

        public ILogin Model { get; set; }

        public ObjectDescriptor Descriptor { get; set; }

        protected override Task OnInitializedAsync()
        {

            Model = Repository.GetNew();
            Descriptor = new ObjectDescriptor(Model, Model.GetType(), TranslateService, ServiceProvider);
            return base.OnInitializedAsync();

        }


    }


}
