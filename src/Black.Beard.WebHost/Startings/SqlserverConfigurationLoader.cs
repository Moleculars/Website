using Bb.Storages.ConfigurationProviders.SqlServer;
using Bb.WebClient.Startings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace Bb.WebHost.Startings
{
    public class SqlserverConfigurationLoader : IConfigurationLoader
    {


        public Loader Configuration { get; set; }

        public bool ConfigLoaded { get; private set; }

        public void Load(InitializationLoader builder)
        {
            var provider = SqlServerConfigurationBuilderExtensions.GetSqlServerProvider(builder.InitialConnection);
            builder.ConfigurationBuilder.Add(provider);
            builder.Builder.Services.Add(ServiceDescriptor.Singleton(provider.DataAccess));
        }


        private const string connectionString = "configConnexionString";

    }

}
