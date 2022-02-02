using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Sql
{


    public class ConnectionStringSetting
    {

        static ConnectionStringSetting()
        {
            string invariantName = nameof(Microsoft.Data.SqlClient.SqlClientFactory);
            if (!DbProviderFactories.GetProviderInvariantNames().Any(c => c == invariantName))
                DbProviderFactories.RegisterFactory(invariantName, Microsoft.Data.SqlClient.SqlClientFactory.Instance);
        }

        public ConnectionStringSetting()
        {
        }              

        //public ConnectionStringSetting(string name, string connectionString, string providerName)
        //    : this()
        //{
        //    Name = name;
        //    ConnectionString = connectionString;
        //    ProviderName = providerName;
        //}

        [Description("p:ConnectionStringSetting,l:en-us,k:Name,d:Name of the connection string")]
        [DisplayName("p:ConnectionStringSetting,l:en-us,k:Name,d:Connection string name")]
        public string Name { get; set; }

        [Description("p:ConnectionStringSetting,l:en-us,k:ConnectionStringValueDescription,d:Connection string value for connecting data")]
        [DisplayName("p:ConnectionStringSetting,l:en-us,k:ConnectionStringValueDisplay,d:Connection string value")]
        public string ConnectionString { get; set; }

        [Description("p:ConnectionStringSetting,l:en-us,k:Name,d:Name of the data provider")]
        [DisplayName("p:ConnectionStringSetting,l:en-us,k:ConnectionStringValue,d:Provider name")]
        [Bb.ComponentModel.DataAnnotations.ListProvider(typeof(DbProviderListProvider))]
        public string ProviderName { get; set; }


        public DbProviderFactory GetProvider()
        {
            return DbProviderFactories.GetFactory(ProviderName);
        }


    }
}
