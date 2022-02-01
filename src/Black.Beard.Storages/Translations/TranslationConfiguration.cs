using Bb.Attributes;
using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.Configurations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bb.Translations
{


    [ExposeClass(ConstantsCore.Configuration, LifeCycle = IocScopeEnum.Transiant, ConfigurationKey = "TranslationsConfiguration")]
    [TranslationKey(TranslationConfiguration.MenuList, "p:TypeName,k:Translations,l:en-us,d:Translations")]
    public class TranslationConfiguration
    {

        public TranslationConfiguration()
        {

            //if (mapper == null)
            //    throw new ArgumentNullException(nameof(mapper));

            //mapper.Build(this);

        }

        // [DisplayTextArea]
        [Description("k:TranslationConnectionString,l:en-us,d:Connection string for connect to sqlserver database translations")]
        [DefaultValue("Server=.;Database=BaseWebsite;Integrated Security=SSPI;Encrypt=true; TrustServerCertificate=true;")]
        public string ConnectionString { get; set; }


        internal const string MenuList = "MenuList";

    }


}
