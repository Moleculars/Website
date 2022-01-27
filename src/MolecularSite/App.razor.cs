using Bb.Services;
using System.Reflection;

namespace MolecularSite
{

    public partial class App
    {

        public App()
        {

        }


        public Assembly[] lazyLoadedAssemblies
        {
            get
            {
                return BlazorAssemblyResolver.Instance.GetAssemblies();
            }
        }



    }


}
