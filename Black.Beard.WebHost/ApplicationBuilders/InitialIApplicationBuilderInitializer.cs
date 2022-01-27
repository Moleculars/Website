using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.Configurations;
using Bb.WebHost.UIComponents;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bb.WebHost.ApplicationBuilders
{


    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(IApplicationBuilderInitializer), LifeCycle = IocScopeEnum.Transiant)]
    public class InitialIApplicationBuilderInitializer : IApplicationBuilderInitializer
    {

        public InitialIApplicationBuilderInitializer()
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
