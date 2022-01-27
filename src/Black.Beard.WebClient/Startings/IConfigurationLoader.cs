
namespace Bb.WebClient.Startings
{


    public interface IConfigurationLoader
    {

        void Load(InitializationLoader builder);


        Loader Configuration { get; set; }


        public bool ConfigLoaded { get; }


    }




}
