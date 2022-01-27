using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.ComponentModel.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Bb.WebHost.Startings
{

    public static class LoadConfigurationInIocExtension
    {


        /// <summary>
        /// Append dynamicaly ExposeClass(Context = "Configuration", DisplayName = "key in configuration") in the thype
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static KeyValuePair<Type, HashSet<ExposeClassAttribute>>[] ResolveModelsConfigurations(this IConfiguration configuration, string outPath)
        {

            var types = ExposedTypes.Instance;

            // Add configuration type manualy. 
            var _configuration = typeof(ExposedTypeConfigurations)
                    .GetMappedConfiguration(configuration) as ExposedTypeConfigurations;

            if (_configuration != null)
                types.Add(_configuration)
                     .AddAttributesInTypeDescriptors();

            // Add configuration type by attribute in code. 
            types.Remove(typeof(ExposedTypeConfigurations));
            KeyValuePair<Type, HashSet<ExposeClassAttribute>>[] configs = types.GetTypes(ConstantsCore.Configuration).ToArray();

            //if (!string.IsNullOrEmpty(outPath))
            //    RegenerateSchemas(types, outPath);

            return configs;

        }

        public static object GetMappedConfiguration(this Type self, IConfiguration configuration)
        {

            var arr = TypeDescriptor.GetAttributes(self)
                .OfType<ExposeClassAttribute>().ToArray();

            if (arr.Length > 0)
            {
                string configurationKey = arr[0].ConfigurationKey;
                if (!string.IsNullOrWhiteSpace(configurationKey))
                {
                    object _configuration = Activator.CreateInstance(self, new object[] { });
                    configuration.Bind(configurationKey, _configuration);
                    return _configuration;
                }
            }

            return null;

        }


        /// <summary>
        /// Register all configurations in the list exposedType)
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void RegisterConfigurations(this IServiceCollection services
            , IConfiguration configuration
            , KeyValuePair<Type, HashSet<ExposeClassAttribute>>[] configs
            , ExposedTypeRepository exposedType)
        {

            foreach (var _type in configs)
                foreach (var attribute in _type.Value)
                    AppendConfiguration(services, configuration, _type.Key, attribute, exposedType);
        }


        /// <summary>
        /// Append configuration in Ioc
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="type">Type that expose attribute <see cref="ExposeClassAttribute"/></param>
        /// <param name="attribute"></param>
        private static void AppendConfiguration(IServiceCollection services, IConfiguration configuration, Type type, ExposeClassAttribute attribute, ExposedTypeRepository exposedTypes)
        {

            var typeExposed = attribute.ExposedType ?? type;                    // resolve type that must be added in the Ioc
            string name = string.IsNullOrEmpty(attribute.ConfigurationKey)      // resolve the key name that must be matched in the configurations
                ? CleanName(attribute.ExposedType.Name)
                : attribute.ConfigurationKey
                ;

            ConstructorInfo[] ctors = type.GetConstructors();
 
            if (ctors.Length > 1)
            {

            }
            else if (ctors.Length == 1)
            {
                ConstructorInfo ctor = ctors[0];
                if (ctor.GetParameters().Length == 0)
                {

                }
                else
                {

                }
            }
            else
            {

            }

            CreateConfigurationDelegate(services, configuration, type, typeExposed, name, exposedTypes, attribute);

        }

        private static void CreateConfigurationDelegate(IServiceCollection services, IConfiguration configuration, Type type, Type typeExposed, string name, ExposedTypeRepository exposedTypes, ExposeClassAttribute attribute)
        {


            IFactory<object> factorySerializableObject = TypeDiscovery.Instance.CreateFromWithTypes<object>(type, new Type[] { });   // create a factory of serializable configuration object

            Func<IServiceProvider, object> func = srvs =>
            {
                var _configuration = factorySerializableObject.Call(null);
                configuration.Bind(name, _configuration);
                return _configuration;
            };

            services.Add(ServiceDescriptor.Transient(type, func));
            Trace.WriteLine($"{name} configuration node mapped on {type}.");
            ExposedType exposed = new ExposedType(type, attribute.Context, func);
            exposedTypes.Add(exposed);

            IFactory<object> factoryExposedConfigurationObject = TypeDiscovery
                .Instance
                .CreateFromWithTypes<object>(typeExposed, new Type[] { type }); // Create a factory of exposed configuration that take the serializable object in the ctor

            if (typeExposed != type && !factoryExposedConfigurationObject.IsEmpty) // If the exposed file is different of the serialized configuration object
            {

                func = srvs =>
                {
                    var model = func(srvs);
                    var _configuration = factoryExposedConfigurationObject.Call(null, model);
                    return _configuration;
                };

                services.Add(ServiceDescriptor.Transient(typeExposed, func));
                Trace.WriteLine($"{name} configuration node mapped on {type}, and exposed in {typeExposed}.");
                //c = new ConfigurationModel(typeExposed, func);

            }

        }

    
    }


}
