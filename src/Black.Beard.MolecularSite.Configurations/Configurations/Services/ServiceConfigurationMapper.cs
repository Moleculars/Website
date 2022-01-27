using Bb.WebHost.Startings;
using Microsoft.Extensions.Configuration;
using System;

namespace Bb.Configurations
{

    public class ServiceConfigurationMapper
    {

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="configuration"></param>
        public ServiceConfigurationMapper(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        private readonly IConfiguration _configuration;


        public void Build(object instance, string keyMapper = null)
        {

            if (string.IsNullOrEmpty(keyMapper))
                keyMapper = CleanName(instance.GetType().Name);

            this._configuration.Bind(keyMapper, _configuration);

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
