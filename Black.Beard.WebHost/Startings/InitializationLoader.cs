using Bb.WebHost.ApplicationBuilders;
using Bb.WebHost.Startings.InitialConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Bb.WebHost.Startings
{


    public class InitializationLoader
    {

        internal InitializationLoader(WebApplicationBuilder builder, IHostEnvironment env, string path, string[] args)
        {
            this.Builder = builder;
            this.HostEnvironment = env;
            this.Path = path;
            this.Arguments = args;
            ExposedTypes = new ExposedTypeRepository();
            Builders = new List<Type>();
            InjectBuilders = new TypesToInject();
            Configurations = new List<Type>();
        }

        public WebApplicationBuilder Builder { get; }

        public IHostEnvironment HostEnvironment { get; }

        public string Path { get; }

        public string[] Arguments { get; }

        public ExposedTypeRepository ExposedTypes { get; }

        public IConfigurationBuilder ConfigurationBuilder { get; internal set; }

        public IConfigurationRoot InitialConfigurationRoot { get; internal set; }

        public List<Type> Builders { get; }

        public List<Type> Configurations { get; }

        public TypesToInject InjectBuilders { get; }
        
        public _InitialConfiguration InitialConfiguration { get; internal set; }
        public WebApplicationBuilder WebApplication { get; internal set; }
        public List<IApplicationBuilderInitializer> InstancesBuilders { get; internal set; }
    }


}
