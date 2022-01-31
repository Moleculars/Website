using Bb.ComponentModel;
using Bb.Configurations.services;
using Bb.WebClient.UIComponents;
using Microsoft.AspNetCore.Components;

namespace Bb.Configurations.Pages
{

    public partial class ConfigurationComponentView
    {


        [Inject]
        public TranslateService TranslateService { get; set; }


        [Inject]
        public ServiceConfigurationRepository? ServiceConfigurations { get; set; }

        [Inject]
        public ServiceConfigurationMapper ServiceConfigurationMapper { get; set; }


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

        public void Save()
        {
            if (ServiceConfigurations != null)
            {
                var sectionName = ServiceConfigurationMapper.GetDefaultSectionName(CurrentItem.GetType());
                ServiceConfigurations.Save(CurrentItem, sectionName);
            }
        }

        public void Cancel()
        {


        }



    }

}
