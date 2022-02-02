using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.ComponentModel.Translations;
using Bb.Configurations;
using Bb.Translations.Services;
using Bb.WebClient.ApplicationBuilders;
using Bb.WebClient.UIComponents;
using System.Data.Common;

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
            //services.AddSingleton(typeof(ITranslateService));
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
