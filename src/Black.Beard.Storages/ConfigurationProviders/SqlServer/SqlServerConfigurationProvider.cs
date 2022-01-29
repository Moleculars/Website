using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using Bb.ComponentModel.Attributes;
using Bb.ComponentModel;

namespace Bb.Storages.ConfigurationProviders.SqlServer
{


    public class SqlServerConfigurationProvider : ConfigurationProvider, IDisposable
    {

        public SqlServerConfigurationProvider(SqlServerConfigurationDataAccess dataAccess)
        {

            _dataAccess = dataAccess;

            if (_dataAccess.SqlServerWatcher != null)
                _changeTokenRegistration = ChangeToken.OnChange(() => _dataAccess.SqlServerWatcher.Watch(), Load);

        }


        public override void Load()
        {

            var datas = _dataAccess.LoadConfigurations();

            var dic = new Dictionary<string, string>();
            foreach (var item in datas.Values)
                dic.Add(item.SectionName, item.Value);

            Data = dic;

        }

        public void Dispose()
        {
            _changeTokenRegistration?.Dispose();
            _dataAccess.SqlServerWatcher?.Dispose();
        }



        private readonly SqlServerConfigurationDataAccess _dataAccess;
        private readonly IDisposable _changeTokenRegistration;




    }
}




