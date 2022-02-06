using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.ComponentModel.DataAnnotations;
using Bb.Configurations;
using Bb.Sql;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Bb.Translations
{


    [ExposeClass(ConstantsCore.Initialization, LifeCycle = IocScopeEnum.Transiant, ConfigurationKey = "TranslationsConfiguration")]
    [TranslationKey(TranslationConfiguration.MenuList, "p:TypeName,k:Translations,l:en-us,d:Manage translation's configuration")]
    public class TranslationConfiguration
    {


        public TranslationConfiguration(InitialeConnectionStringSetting initialeConnectionStringSetting)
        {
            this._settings = initialeConnectionStringSetting;
            this.Cultures = new List<CultureInfo>()
            {
                CultureInfo.GetCultureInfo("en-US"),
                CultureInfo.GetCultureInfo("fr-FR"),
            };
        }


        [Description("p:Translation,k:TableName,l:en-us,d:Name of the table That contains translations")]
        [DefaultValue("Server=.;Database=BaseWebsite;Integrated Security=SSPI;Encrypt=true; TrustServerCertificate=true;")]
        public string TableName { get; set; } = "translations";


        [ListProvider(typeof(CultureProviderList))]
        public List<CultureInfo> Cultures { get; set; }


        public ConnectionStringSetting GetConnection()
        {
            return _settings;
        }

        internal const string MenuList = "MenuList";
        private readonly InitialeConnectionStringSetting _settings;

    }

}
