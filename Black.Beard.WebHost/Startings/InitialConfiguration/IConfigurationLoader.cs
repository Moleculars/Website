
namespace Bb.WebHost.Startings.InitialConfiguration
{


    public interface IConfigurationLoader
    {

        void Load(InitializationLoader builder);


        _Loader Configuration { get; set; }


        public bool ConfigLoaded { get; }


    }




}
