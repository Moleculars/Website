using Bb.Attributes;
using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bb.Configurations
{


    [ExposeClass(ConstantsCore.Configuration, LifeCycle = IocScopeEnum.Transiant, ConfigurationKey = "TestConfiguration")]
    [TranslationKey(WebClientConstants.MenuList, "::TestTransiantConfiguration::TypeName::Test configuration transiante")]
    public class TestTransiantConfiguration
    {

        public TestTransiantConfiguration(ServiceConfigurationMapper? mapper)
        {

            //Tests = new List<ItemTestConfiguration>();

            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            mapper.Build(this);

        }


        [Description("::TestTransiantConfiguration::Name::Name of the configuration")]
        public string? Name { get; set; }


        //[Description("::TestTransiantConfiguration::SelectAnItem::Select an Item")]
        //public Enum1 MyEnum { get; set; }


        ////[Description("::TestTransiantConfiguration::Counter::version of the configuration")]
        ////[DefaultValue(-1)]
        ////[Range(-1, 200)]
        ////[StepNumeric(0.2f)]
        ////public int? Counter { get; set; }


        //[Description("::TestTransiantConfiguration::Date::date of the configuration")]
        //[DefaultValue("2/11/1971")]
        //[DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        //public DateTime? Date { get; set; }

        [Description("::TestTransiantConfiguration::SartDate::Start at")]
        public TimeSpan? StartAt { get; set; }

        //[Description("::TestTransiantConfiguration::Checkbox::This is a checkbox")]
        //public bool Checkbox { get; set; }

        public List<ItemTestConfiguration> Tests { get; set; }


    }


    public enum Enum1
    {

        [TranslationKey("::TestTransiantConfiguration::Value1::Valeur 1")]
        Value1,

        [TranslationKey("::TestTransiantConfiguration::Value2::Valeur 2")]
        Value2,

        Value3,


    }


    public class ItemTestConfiguration
    {

        [Description("::TestTransiantConfiguration::Name::Name of the configuration")]
        public string? Name { get; set; }

    }

}
