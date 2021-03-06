using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using System;

namespace Bb.Configurations
{


    public class ServiceConfigurationMapper
    {

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="configuration"></param>
        public ServiceConfigurationMapper(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;


        public void Build(object instance, string keyMapper = null)
        {

            if (string.IsNullOrEmpty(keyMapper))
                keyMapper = GetDefaultSectionName(instance.GetType());

            var datas = _configuration.GetSection(keyMapper);
            if (datas.Value != null)
                ContentHelper.Map(instance, datas.Value);

        }

        public string GetDefaultSectionName(Type type)
        {
            var keyMapper = type.GetAttributes<ExposeClassAttribute>()
                    .FirstOrDefault()?.ConfigurationKey
                ?? CleanName(type.Name);

            return keyMapper;
        }

        private static string CleanName(string txt)
        {

            if (txt.Contains('`'))
            {
                var index = txt.IndexOf("`");
                return txt[..index];
            }

            return txt;

        }


    }



}
