﻿using Bb.ComponentModel;
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

        public void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(typeof(ServiceConfigurationRepository));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }


        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }


    }

}
