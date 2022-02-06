using Bb.Storages.ConfigurationProviders.SqlServer;
using Bb.WebClient.Startings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bb.WebHost.Startings
{
    public class SqlserverConfigurationLoader : IConfigurationLoader
    {


        public Loader Configuration { get; set; }

        public bool ConfigLoaded { get; private set; }

        public void Load(InitializationLoader builder)
        {

            IConfigurationBuilder configuration = builder.Builder.Configuration;
            var provider = SqlServerConfigurationBuilderExtensions.GetSqlServerProvider(builder.InitialConnection);
         
            configuration.Add(provider);
            builder.Builder.Services.Add(ServiceDescriptor.Singleton(provider.DataAccess));

        }


        private const string connectionString = "configConnexionString";

    }

}
