using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.Configurations;
using Bb.WebClient.ApplicationBuilders;
using Bb.WebClient.UIComponents;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bb.WebHost.ApplicationBuilders
{


    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(IApplicationBuilderInitializer), LifeCycle = IocScopeEnum.Transiant)]
    public class InitialIApplicationBuilderInitializer : IApplicationBuilderInitializer
    {

        public InitialIApplicationBuilderInitializer()
        {

        }

        public bool CanInitialize(WebApplicationBuilder builder)
        {
            return true;
        }

        public void Initialize(WebApplicationBuilder builder)
        {
            var services = builder.Services;
            
            services.AddSingleton(typeof(UIService));
            services.AddHttpClient();

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
