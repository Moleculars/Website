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

            var cnxString = Environment.GetEnvironmentVariable(connectionString);
            if (string.IsNullOrEmpty(cnxString))
                Trace.WriteLine($"no connection string specified in environment variable.Please if you want to use a sqlserver provider, specify a environement variable '{connectionString}'");
            else
            {
                var provider = SqlServerConfigurationBuilderExtensions.GetSqlServerProvider(cnxString, 30);
                builder.ConfigurationBuilder.Add(provider);
                builder.Builder.Services.Add(ServiceDescriptor.Singleton(provider.DataAccess));
            }

        }


        private const string connectionString = "configConnexionString";

    }

}
