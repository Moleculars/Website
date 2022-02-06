using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.Configurations.services;
using Bb.WebClient.ApplicationBuilders;

namespace Bb.Configurations.Builders
{


    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(IApplicationBuilderInitializer), LifeCycle = IocScopeEnum.Transiant)]
    public class ConfigurationBuilder : IApplicationBuilderInitializer
    {


        public ConfigurationBuilder()
        {

        }


        public bool CanInitialize(WebApplicationBuilder builder)
        {
            return true;
        }


        public void Initialize(WebApplicationBuilder builder)
        {

            var services = builder.Services;
         
            services.AddSingleton(typeof(ServiceConfigurationRepository));

        }



        public bool CanConfigure(IApplicationBuilder app)
        {
            return true;
        }


        public void Configure(IApplicationBuilder app)
        {

        }


    }

}
