using Bb.Configurations;
using Bb.WebClient.UIComponents;
using BlazorPropertyGridComponents.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Bb.MolecularSite.Configurations.Pages
{

    public partial class ConfigurationComponentView
    {


        [Inject]
        public TranslateService TranslateService { get; set; }


        [Inject]
        public ServiceConfigurationRepository? ServiceConfigurations { get; set; }


        [Inject]
        public IJSRuntime JsRunTime { get; set; }


        public ItemEnumerable[]? Configurations { get; set; }
               
        public object CurrentItem { get; set; }


        protected override Task OnInitializedAsync()
        {

            var result = base.OnInitializedAsync();

            if (ServiceConfigurations != null)
                Configurations = ServiceConfigurations.GetConfigurationsTypes();

            return result;

        }


        public void Selected(ItemEnumerable type)
        {

            if (ServiceConfigurations != null)
                if (type is ItemEnumerable<Type> t)
                {
                    var configuration = ServiceConfigurations.Get(t.Tag);
                    CurrentItem = configuration;
                }

        }
              

    }

}
