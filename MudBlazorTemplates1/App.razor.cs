using Bb.Services;
using System.Reflection;

namespace MudBlazorTemplates1
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
