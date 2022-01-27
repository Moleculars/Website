using Bb.Services;
using System.Reflection;

namespace MolecularSite
{

    public partial class App
    {

        public App()
        {
            _currentAssembly = typeof(App).Assembly;
        }


        public Assembly[] lazyLoadedAssemblies
        {
            get
            {

                var result = BlazorAssemblyResolver.Instance.GetAssemblies()
                    .Where(c => c != _currentAssembly)
                    .ToArray();

                return result;

            }
        }


        private readonly Assembly _currentAssembly;


    }


}
