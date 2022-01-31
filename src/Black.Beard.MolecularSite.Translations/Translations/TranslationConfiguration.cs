using Bb.Attributes;
using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bb.Configurations
{


    [ExposeClass(ConstantsCore.Configuration, LifeCycle = IocScopeEnum.Transiant, ConfigurationKey = "TranslationsConfiguration")]
    [TranslationKey(WebClientConstants.MenuList, "::Translations::TypeName::Translations")]
    public class TranslationConfiguration
    {

        public TranslationConfiguration(ServiceConfigurationMapper? mapper)
        {

            //Tests = new List<ItemTestConfiguration>();

            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            mapper.Build(this);

        }

    }


}
