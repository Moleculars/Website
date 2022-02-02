using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using System.Data.Common;

namespace Bb.Sql
{


    [ExposeClass(ConstantsCore.Configuration, ExposedType = typeof(ConnectionSettings), ConfigurationKey = "ConnectionStringSettings", LifeCycle = IocScopeEnum.Transiant)]
    public partial class ConnectionSettings
    {

        public ConnectionSettings()
        {
            this.ConnectionStringSettings = new ConnectionStringSettings();
        }

        public ConnectionStringSettings ConnectionStringSettings { get;set; }


        public void Factories()
        {

            var item = DbProviderFactories.GetProviderInvariantNames();
            foreach (var itemName in item)
            {

            }

        }


    }
}
