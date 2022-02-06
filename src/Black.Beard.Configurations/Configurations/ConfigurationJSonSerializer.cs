using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using System.Diagnostics;

namespace Bb.Configurations
{

    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(ConfigurationJSonSerializer))]
    public class ConfigurationJSonSerializer : ConfigurationSubSerializer
    {

        public ConfigurationJSonSerializer(Microsoft.Extensions.Configuration.IConfiguration configuration) 
            : base(configuration)
        {

        }

        public override bool CanMap(Type type)
        {
            return true;
        }

        public override void Map(object instance, string keyMapper)
        {
            var datas = _configuration.GetSection(keyMapper);
            if (datas != null && datas.Value != null)
            {
                try
                {
                    instance.Map(datas.Value);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                }
            }
        }

    }

}
