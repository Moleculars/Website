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

        public ConnectionStringSetting()
        {
        }              

        public ConnectionStringSetting(string name, string connectionString, string providerName)
            : this()
        {
            Name = name;
            ConnectionString = connectionString;
            ProviderName = providerName;
        }

        [Description("p:ConnectionStringSetting,l:en-us,k:Name,d:Name")]
        [DisplayName("p:ConnectionStringSetting,l:en-us,k:Name,d:Name")]
        public string Name { get; set; }

        [Description("")]
        [DisplayName("")]
        public string ConnectionString { get; set; }

        [Description("")]
        [DisplayName("")]
        public string ProviderName { get; set; }


        public DbProviderFactory GetProvider()
        {
            return DbProviderFactories.GetFactory(ProviderName);
        }


    }
}
