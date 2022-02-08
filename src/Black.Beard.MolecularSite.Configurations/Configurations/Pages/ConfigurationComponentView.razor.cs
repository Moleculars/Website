using Bb.ComponentModel;
using Bb.ComponentModel.Translations;
using Bb.Configurations.services;
using Bb.WebClient.UIComponents;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Bb.Configurations.Pages
{

    public partial class ConfigurationComponentView
    {


        [Inject]
        public ITranslateService? TranslateService { get; set; }

        [Inject]
        public ServiceConfigurationRepository? ServiceConfigurations { get; set; }

        [Inject]
        public ConfigurationSerializer? ConfigurationSerializer { get; set; }


        public ItemEnumerable[]? Configurations { get; set; }

        public object? CurrentItem { get; set; }

        protected override Task OnInitializedAsync()
        {

            var result = base.OnInitializedAsync();

            if (ServiceConfigurations != null)
                Configurations = ServiceConfigurations.GetConfigurationsTypes();

            return result;

        }


        public void Selected(ItemEnumerable type)
        {

            if (ServiceConfigurations != null && ConfigurationSerializer != null)
                if (type is ItemEnumerable<Type> t && t != null)
                    CurrentItem = ConfigurationSerializer.Get<object>(t.Tag);

        }

        public void Save()
        {
            if (ServiceConfigurations != null && CurrentItem != null)
            {
                var sectionName = ConfigurationSerializer?.GetConfigurationKey(CurrentItem.GetType());
                if (!string.IsNullOrEmpty(sectionName))
                    ServiceConfigurations.Save(CurrentItem, sectionName);
                else
                {

                }
            }
        }

        public void Cancel()
        {


        }

    }

}
