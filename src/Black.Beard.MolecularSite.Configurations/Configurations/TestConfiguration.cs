using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using System;

namespace Bb.Configurations
{


    [ExposeClass(ConstantsCore.Configuration, LifeCycle = IocScopeEnum.Transiant)]
    [TranslationKey(WebClientConstants.MenuList, "::TestTransiantConfiguration::TypeName::Test configuration transiante")]
    public class TestTransiantConfiguration
    {

        public TestTransiantConfiguration(ServiceConfigurationMapper? serviceBuilder)
        {
            
            if (serviceBuilder == null)
                throw new ArgumentNullException(nameof(serviceBuilder));

            serviceBuilder.Build(this);

        }

        [TranslationKey("::TestTransiantConfiguration::TypeName::Test configuration transiante")]
        public string? Name { get; set; }

    }

}
