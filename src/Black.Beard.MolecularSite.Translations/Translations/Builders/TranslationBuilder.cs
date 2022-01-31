using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.Configurations;
using Bb.WebClient.ApplicationBuilders;
using Bb.WebClient.UIComponents;


namespace Bb.Translations.Builders
{


    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(IApplicationBuilderInitializer), LifeCycle = IocScopeEnum.Transiant)]
    public class TranslationBuilder : IApplicationBuilderInitializer
    {

        public TranslationBuilder()
        {

        }

        public void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton(typeof(ServiceConfigurationMapper));
            //services.AddSingleton(typeof(ServiceConfigurationRepository));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }


        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }


    }

}
