using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.Sql;
using Bb.WebClient.UIComponents;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Translations
{

    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(ITranslateServiceDataAccess), LifeCycle = IocScopeEnum.Singleton)]
    public class TranslateServiceDataAccess : ITranslateServiceDataAccess
    {

        public TranslateServiceDataAccess(TranslationConfiguration configuration)
        {

            this._configuration = configuration;
            //this._sql = new Sql.SqlProcessor(this._configuration.ConnectionString, SqlClientFactory.Instance);
        }


        private readonly TranslationConfiguration _configuration;
        private readonly SqlProcessor _sql;

    }

}
