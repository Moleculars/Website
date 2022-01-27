using Bb.WebClient.ApplicationBuilders;


namespace Bb.WebClient.Startings
{


    public class InitializationLoader
    {

        public InitializationLoader(WebApplicationBuilder builder, IHostEnvironment env, string path, string[] args)
        {
            this.Builder = builder ?? throw new NullReferenceException(nameof(builder));
            this.HostEnvironment = env ?? throw new NullReferenceException(nameof(env));
            this.Path = path ?? throw new NullReferenceException(nameof(path));
            this.Arguments = args ?? throw new NullReferenceException(nameof(args));
            ExposedTypes = new ExposedTypeRepository();
            Builders = new List<Type>();
            InjectBuilders = new TypesToInject();
            Configurations = new List<Type>();
            InstancesBuilders = new();
        }

        public WebApplicationBuilder Builder { get; }

        public IHostEnvironment HostEnvironment { get; }

        public string Path { get; }

        public string[] Arguments { get; }

        public ExposedTypeRepository ExposedTypes { get; }

        public IConfigurationBuilder? ConfigurationBuilder { get; set; }

        public IConfigurationRoot? InitialConfigurationRoot { get; set; }

        public List<Type> Builders { get; }

        public List<Type> Configurations { get; }

        public TypesToInject InjectBuilders { get; }
        
        public InitialConfiguration? InitialConfiguration { get; set; }

        public List<IApplicationBuilderInitializer> InstancesBuilders { get; }

    }


}
