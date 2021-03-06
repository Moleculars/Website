using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bb.WebClient.Startings;
using Bb.WebClient.ApplicationBuilders;
using Bb.Configurations;
using Bb.Storages.ConfigurationProviders.SqlServer;
using Bb.Sql;

namespace Bb.WebHost.Startings
{


    public static class InitializationLoaderInitializer
    {


        public static InitializationLoader LoadConfiguration(this WebApplicationBuilder builder, string[] args)
        {

            IHostEnvironment env = builder.Environment;
            ConfigurationManager conf = builder.Configuration;

            string path = Environment.CurrentDirectory;
            conf.SetBasePath(path);

            var result = new InitializationLoader(builder, env, path, args)
            {
                InitialConfigurationRoot = conf,
            }
            .LoadPrimaryConfiguration()
            .LoadAbstractLoaders()
            .LoadAssemblies()
            .ResolveInjections()
            .InjectServices()
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

                    if (Activator.CreateInstance(type) is not IConfigurationLoader srv)
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

        /// <summary>
        /// Inject collected services
        /// </summary>
        /// <param name="self"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        public static InitializationLoader ResolveBuilders(this InitializationLoader self)
        {

            // Inject individual types in the ioc
            foreach (var item in self.Builders)
            {

                IApplicationBuilderInitializer instance;
                List<object> args = new List<object>();

                var ctor = item.GetConstructors()[0];
                var parameters = ctor.GetParameters();
                foreach (var parameter in parameters)
                    if (parameter.ParameterType == typeof(InitializationLoader))
                        args.Add(self);

                instance = (IApplicationBuilderInitializer)Activator.CreateInstance(item, args.ToArray());

                self.InstancesBuilders.Add(instance);

            }

            var builder = self.Builder;

            var items = self.InstancesBuilders.ToList();
            var max = items.Count;
            int count = 0;
            var toRemove = new List<IApplicationBuilderInitializer>();


            while (items.Count > 0 && count < max)
            {

                foreach (var item in self.InstancesBuilders)
                    if (item.CanInitialize(builder))
                    {
                        item.Initialize(builder);
                        toRemove.Add(item);
                    }

                foreach (var item in toRemove)
                    items.Remove(item);

                toRemove.Clear();

            }

            return self;

        }

        private static InitializationLoader InjectServices(this InitializationLoader self)
        {

            IServiceCollection services = self.Builder.Services;

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

            return self;

        }

        private static InitializationLoader LoadAssemblies(this InitializationLoader self)
        {

            TypeDiscovery.Instance.LoadAssembliesFromFolders();

            return self;

        }
        private static InitializationLoader ResolveInjections(this InitializationLoader self)
        {

            CollecteInitializationTypes(self);

            var types = ExposedTypes.Instance;

            // Inject the first sql connection
            self.InjectBuilders.Add(IocScopeEnum.Singleton, typeof(InitialeConnectionStringSetting), self.InitialConnection.GetConnection());
            self.InjectBuilders.Add(IocScopeEnum.Singleton, typeof(BaseConfiguration), self.InitialConnection);


            var _allconfigurations = types.GetTypes(ConstantsCore.Configuration).ToList();
            foreach (var item1 in _allconfigurations)
                foreach (var item2 in item1.Value)
                {

                    var t = (item2.ExposedType ?? item1.Key);
                    if (self.Configurations.Add(t))
                    {
                        if (item1.Key.GetConstructor(new Type[] { }) != null)
                            self.InjectBuilders.Add(item2.LifeCycle, t, (s) =>
                            {
                                var mapper = s.GetService(typeof(ConfigurationSerializer)) as ConfigurationSerializer;
                                return mapper.Get<object>(item1.Key);
                            });
                        else
                            self.InjectBuilders.Add(item2.LifeCycle, t, item1.Key);
                    }

                }

            self.InjectBuilders.Add(IocScopeEnum.Singleton, typeof(ExposedTypeRepository), self.ExposedTypes);
            self.InjectBuilders.Add(IocScopeEnum.Singleton, typeof(InitializationLoader), self);

            return self;

        }

        private static List<KeyValuePair<Type, HashSet<ExposeClassAttribute>>> CollecteInitializationTypes(InitializationLoader self)
        {

            var _allBuilders = ExposedTypes.Instance.GetTypes(ConstantsCore.Initialization).ToList();
            var toRemove = new List<KeyValuePair<Type, HashSet<ExposeClassAttribute>>>();
            var _types = new HashSet<Type>();

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


            var _configuration = new InitialConfiguration();
            self.InitialConfigurationRoot.Bind(typeof(InitialConfiguration).Name, _configuration);

            _configuration.Loaders.Add(new Loader() { Type = typeof(FolderConfigurationLoader).AssemblyQualifiedName });
            _configuration.Loaders.Add(new Loader() { Type = typeof(SqlserverConfigurationLoader).AssemblyQualifiedName });

            self.InitialConfiguration = _configuration;


            var cnxString = Environment.GetEnvironmentVariable(connectionString);

            if (string.IsNullOrEmpty(cnxString))
            {
                Trace.WriteLine($"no connection string specified in environment variable.Please if you want to use a sqlserver provider, specify a environement variable '{connectionString}'");
            }


            self.InitialConnection = new BaseConfiguration(new Sql.InitialeConnectionStringSetting()
            {
                Name = "InitialConnexion",
                ConnectionString = cnxString,
                ProviderName = "SqlClientFactory"
            });


            // Add the first configuration that not exposed by attribute
            self.ExposedTypes.Add(
                new ExposedType(typeof(InitialConfiguration), ConstantsCore.Configuration, srv => self.InitialConfiguration)
                );


            return self;

        }

        private const string connectionString = "configConnexionString";

    }




}
