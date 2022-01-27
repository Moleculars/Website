using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.Configurations;
using Bb.WebClient.ApplicationBuilders;
using Bb.WebClient.UIComponents;


namespace Bb.WebHost.ApplicationBuilders
{


    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(IApplicationBuilderInitializer), LifeCycle = IocScopeEnum.Transiant)]
    public class ConfigurationBuilder : IApplicationBuilderInitializer
    {

        public ConfigurationBuilder()
        {

        }

        public void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(typeof(UIService));
            services.AddSingleton(typeof(ServiceConfigurationMapper));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }


        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }


    }

}
