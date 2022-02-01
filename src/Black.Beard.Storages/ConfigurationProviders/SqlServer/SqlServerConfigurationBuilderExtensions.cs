using Microsoft.Extensions.Configuration;


namespace Bb.Storages.ConfigurationProviders.SqlServer
{

    /// <summary>
    /// https://mousavi310.github.io/posts/a-refreshable-sql-server-configuration-provider-for-net-core/
    /// </summary>
    public static class SqlServerConfigurationBuilderExtensions
    {

        public static IConfigurationBuilder AddSqlServer(this IConfigurationBuilder builder, string connectionString, int refreshInterval = 1 * 60)
        {
            var dataAccess = new SqlServerConfigurationDataAccess(connectionString, refreshInterval);
            var source = new SqlServerConfigurationSource(dataAccess);
            return builder.Add(source);
        }

        public static SqlServerConfigurationSource GetSqlServerProvider(string connectionString, int refreshInterval = 1 * 60)
        {
            var dataAccess = new SqlServerConfigurationDataAccess(connectionString, refreshInterval);
            var source = new SqlServerConfigurationSource(dataAccess);
            return source;

        }
    }

}