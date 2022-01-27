using Bb.WebHost.Startings.InitialConfiguration;
using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Bb.WebHost.ApplicationBuilders;

namespace Bb.WebHost.Startings
{


    public static class InitializationLoaderInitializer
    {

        /// <summary>
        /// Inject collected services
        /// </summary>
        /// <param name="self"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        public static InitializationLoader ResolveBuilders(this InitializationLoader self)
        {


            var services = self.Builder.Services;


            // Inject individual types in the ioc
            foreach (var item in self.InjectBuilders)
            {

                switch (item.LifeCycle)
                {

                    case IocScopeEnum.Singleton:
                        if (item.ImplementationType != null)
                            services.Add(ServiceDescriptor.Singleton(item.Type, item.ImplementationType));

                        else if (item.Instance != null)
                            services.Add(ServiceDescriptor.Singleton(item.Type, item.Instance));

                        else if (item.Function != null)
                            services.Add(ServiceDescriptor.Singleton(item.Type, item.Function));

                        else
                        {

                        }
                        break;

                    case IocScopeEnum.Scoped:
                        if (item.ImplementationType != null)
                            services.Add(ServiceDescriptor.Scoped(item.Type, item.ImplementationType));

                        else if (item.Instance != null)
                            services.Add(ServiceDescriptor.Scoped(item.Type, (c) => item.Instance));

                        else if (item.Function != null)
                            services.Add(ServiceDescriptor.Scoped(item.Type, item.Function));

                        else
                        {

                        }

                        break;

                    case IocScopeEnum.Transiant:
                    default:

                        if (item.ImplementationType != null)
                            services.Add(ServiceDescriptor.Transient(item.Type, item.ImplementationType));

                        else if (item.Instance != null)
                            services.Add(ServiceDescriptor.Transient(item.Type, (c) => item.Instance));

                        else if (item.Function != null)
                            services.Add(ServiceDescriptor.Transient(item.Type, item.Function));

                        else
                        {

                        }
                        break;

                }

            }

            List<IApplicationBuilderInitializer> _builderlist = new List<IApplicationBuilderInitializer>();

            // Inject individual types in the ioc
            foreach (var item in self.Builders)
                _builderlist.Add((IApplicationBuilderInitializer)Activator.CreateInstance(item));

            var configuration = self.InitialConfigurationRoot;

            foreach (var item in _builderlist)
                item.Initialize(services, configuration);

            self.InstancesBuilders = _builderlist;

            return self;

        }

        public static InitializationLoader LoadConfiguration(this WebApplicationBuilder builder, string[] args)
        {


            IHostEnvironment env = builder.Environment;
            ConfigurationManager conf = builder.Configuration;

            string path = Environment.CurrentDirectory;
            conf.SetBasePath(path);

            var result = new InitializationLoader(builder, env, path, args)
            {
                ConfigurationBuilder = conf,
                InitialConfigurationRoot = conf,
            }
            .LoadPrimaryConfiguration()
            .LoadAbstractLoaders()
            .LoadAssemblies()
            .ResolveInjections()

            .ResolveBuilders()

            ;

            var assemblyResolver = new Bb.Services.BlazorAssemblyResolver(result);

            return result;

        }

        /// <summary>
        /// Loads the directories json files from configuration.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <param name="self">The self.</param>
        /// <returns></returns>
        public static InitializationLoader LoadAbstractLoaders(this InitializationLoader loader)
        {

            var self = loader.InitialConfiguration;

            if (self.Loaders != null)
            {

                bool configLoaded = false;

                foreach (var item in self.Loaders)
                {

                    var type = Type.GetType(item.Type);
                    if (type == null)
                        throw new NotImplementedException(item.Type);

                    var srv = Activator.CreateInstance(type) as IConfigurationLoader;
                    if (srv == null)
                        throw new InvalidCastException(item.Type);
                    srv.Configuration = item;
                    srv.Load(loader);

                    if (srv.ConfigLoaded)
                        configLoaded = true;

                }

                if (!configLoaded)
                    Trace.WriteLine("no configuration file loaded", TraceLevel.Info.ToString());

            }
            else
                Trace.WriteLine("no configuration folder specified", TraceLevel.Info.ToString());

            return loader;

        }

        private static InitializationLoader LoadAssemblies(this InitializationLoader self)
        {

            TypeDiscovery.Instance.EnsureAllAssembliesAreLoaded();
            TypeDiscovery.Instance.LoadAssembliesFromFolders();

            return self;

        }

        private static InitializationLoader ResolveInjections(this InitializationLoader self)
        {

            CollecteInitializationTypes(self);

            var types = ExposedTypes.Instance;

            var _allconfigurations = types.GetTypes(ConstantsCore.Configuration).ToList();
            foreach (var item1 in _allconfigurations)
                foreach (var item2 in item1.Value)
                {
                    self.InjectBuilders.Add(item2.LifeCycle, item2.ExposedType ?? item1.Key, item1.Key);
                    self.Configurations.Add(item2.ExposedType);
                }

            self.InjectBuilders.Add(IocScopeEnum.Singleton, typeof(ExposedTypeRepository), self.ExposedTypes);
            self.InjectBuilders.Add(IocScopeEnum.Singleton, typeof(InitializationLoader), self);

            return self;

        }

        private static List<KeyValuePair<Type, HashSet<ExposeClassAttribute>>> CollecteInitializationTypes(InitializationLoader self)
        {

            var _allBuilders = ExposedTypes.Instance.GetTypes(ConstantsCore.Initialization).ToList();
            var toRemove = new List<KeyValuePair<Type, HashSet<ExposeClassAttribute>>>();
            HashSet<Type> _types = new HashSet<Type>();

            foreach (var service in _allBuilders.Where(c => typeof(IApplicationBuilderInitializer).IsAssignableFrom(c.Key)))
                if (_types.Add(service.Key))
                {
                    Trace.WriteLine($"builder '{service}' found and loaded", TraceLevel.Info.ToString());
                    self.Builders.Add(service.Key);
                    toRemove.Add(service);
                }


            foreach (var service in _allBuilders.Where(c => typeof(IInjectBuilder).IsAssignableFrom(c.Key)))
                if (_types.Add(service.Key))
                {
                    Trace.WriteLine($"IInjectBuilder '{service.Key}' found and loaded", TraceLevel.Info.ToString());
                    foreach (var expose in service.Value)
                        self.InjectBuilders.Add(expose.LifeCycle, service.Key, service.Key);
                    toRemove.Add(service);
                }

            foreach (var item in toRemove)
                _allBuilders.Remove(item);

            foreach (var item1 in _allBuilders)
                foreach (var item2 in item1.Value)
                    self.InjectBuilders.Add(item2.LifeCycle, item2.ExposedType ?? item1.Key, item1.Key);


            return _allBuilders;

        }

        /// <summary>
        /// Map the configuration
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        private static InitializationLoader LoadPrimaryConfiguration(this InitializationLoader self)
        {


            _InitialConfiguration _configuration = new _InitialConfiguration();
            self.InitialConfigurationRoot.Bind(typeof(_InitialConfiguration).Name, _configuration);


            _configuration.Loaders.Add(new _Loader()
            {
                Type = typeof(FolderConfigurationLoader).AssemblyQualifiedName,
            });

            self.InitialConfiguration = _configuration;

            // Add the first configuration that not exposed by attribute
            self.ExposedTypes.Add(
                new ExposedType(typeof(_InitialConfiguration), ConstantsCore.Configuration, srv => self.InitialConfiguration)
                );

            return self;

        }


    }


}
