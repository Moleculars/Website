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

        public bool CanInitialize(WebApplicationBuilder builder)
        {
            return true;
        }

        public void Initialize(WebApplicationBuilder builder)
        {
            //var services = builder.Services;
            //services.AddSingleton(typeof(ITranslateService));
            //services.AddSingleton(typeof(ServiceConfigurationRepository));


            //services.AddHttpClient<TranslateServiceByRemote, TranslateServiceByRemote>(client =>
            //{
            //    client.BaseAddress = new Uri("https://api-free.deepl.com/v2/translate");
            //});

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
