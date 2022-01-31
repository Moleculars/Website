using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.Configurations;
using Bb.WebClient.UIComponents;
using BlazorPropertyGridComponents.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.ComponentModel;

namespace Bb.Translations.Pages
{

    public partial class TranslationsComponentView
    {


        [Inject]
        public TranslateService TranslateService { get; set; }
            

        protected override Task OnInitializedAsync()
        {

            var result = base.OnInitializedAsync();
                    
            return result;

        }


    }

}
