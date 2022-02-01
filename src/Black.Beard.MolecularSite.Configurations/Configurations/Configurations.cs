using Bb.Attributes;
using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.Translations;
using System.ComponentModel;

namespace Bb.Configurations
{

    [ExposeClass(ConstantsCore.Configuration, ConfigurationKey = "Configurations")]
    [TranslationKey(Configurations.MenuList, "c:TypeName,k:Configurations,l:en-us,d:Configurations")]
    public class Configurations
    {

        public Configurations(ServiceConfigurationMapper? mapper)
        {
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            mapper.Build(this);

            if (Items == null)
                Items = new List<string>();

        }

        //[TypeConverter(typeof(TypeToStringConverter))]
        //[ListProvider(typeof(TypeListProvider))]
        public List<string> Items { get; set;}


        internal const string MenuList = "MenuList";


    }

}
