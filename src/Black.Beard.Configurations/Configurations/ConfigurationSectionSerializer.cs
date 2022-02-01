using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;

namespace Bb.Configurations
{

    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(ConfigurationSectionSerializer))]
    public class ConfigurationSectionSerializer : ConfigurationSubSerializer
    {

        public ConfigurationSectionSerializer(Microsoft.Extensions.Configuration.IConfiguration configuration) 
            : base(configuration)
        {

        }

        public override bool CanMap(Type type)
        {
            return typeof(Microsoft.Extensions.Configuration.ConfigurationSection).IsAssignableFrom(type);
        }

        public override void Map(object instance, string keyMapper)
        {
            var i = (Microsoft.Extensions.Configuration.ConfigurationSection)instance;
            i.Map(_configuration.GetSection(keyMapper).Value);
        }


    }

}
