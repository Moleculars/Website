using Bb.ComponentModel;
using Bb.ComponentModel.Translations;
using Bb.Configurations.services;
using Bb.WebClient.UIComponents;
using Microsoft.AspNetCore.Components;

namespace Bb.Configurations.Pages
{

    public partial class ConfigurationComponentView
    {


        [Inject]
        public ITranslateService TranslateService { get; set; }


        [Inject]
        public ServiceConfigurationRepository? ServiceConfigurations { get; set; }

        [Inject]
        public ConfigurationSerializer ConfigurationSerializer { get; set; }


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
                    var configuration = ConfigurationSerializer.Get<object>(t.Tag);
                    CurrentItem = configuration;
                }

        }

        public void Save()
        {
            if (ServiceConfigurations != null)
            {
                var sectionName = ConfigurationSerializer.GetConfigurationKey(CurrentItem.GetType());
                ServiceConfigurations.Save(CurrentItem, sectionName);
            }
        }

        public void Cancel()
        {


        }



    }

}
