using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Microsoft.Extensions.Configuration;


namespace Bb.Storages.ConfigurationProviders.SqlServer
{


    public class SqlServerConfigurationSource : IConfigurationSource
    {


        public SqlServerConfigurationSource(SqlServerConfigurationDataAccess dataAccess)
        {
            this.DataAccess = dataAccess;
        }


        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new SqlServerConfigurationProvider(this.DataAccess);
        }


        public SqlServerConfigurationDataAccess DataAccess { get; set; }


    }



}