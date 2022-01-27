using Bb.Configurations;
using Microsoft.AspNetCore.Components;

namespace Bb.Pages
{

    public partial class ConfigurationComponentView
    {

        public ConfigurationComponentView()
        {

        }


        [Inject]
        public ServiceConfigurationRepository ServiceConfigurations { get; set; }

    }

}
