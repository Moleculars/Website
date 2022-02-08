using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.CustomComponents;
using System.ComponentModel;

namespace Bb.Translations.Services
{

    [ExposeClass(ConstantsCore.Configuration, ExposedType = typeof(ConfigureTranslateServiceByRemote), LifeCycle = IocScopeEnum.Transiant)]
    public class ConfigureTranslateServiceByRemote
    {


        // string key = "759a5f1d-5d9c-1e20-bfaf-5067e6748c4c:fx";

        [DisplayName("p:ConfigureTranslateServiceByRemote,key:SecurityKey,l:en-us,d:Security key")]
        [Description("p:ConfigureTranslateServiceByRemote,key:DescriptionSecurityKey,l:en-us,d:Security key for authenticate the service on the Deepl api")]
        [Category("p:ConfigureTranslateServiceByRemote,key:CategoryDeepl,l:en-us,d:Deepl Category")]
        public List<Mapper<string>> SecurityKey { get;  set; }

        [DisplayName("p:ConfigureTranslateServiceByRemote,key:UseFreeApi,l:en-us,d:Use free api")]
        [Description("p:ConfigureTranslateServiceByRemote,key:DescriptionUseFreeApi,l:en-us,d:If flaged the service call the free api")]
        [Category("p:ConfigureTranslateServiceByRemote,key:CategoryDeepl,l:en-us,d:Deepl Category")]
        public bool UseFreeApi { get; set; } = true;

    }

}
