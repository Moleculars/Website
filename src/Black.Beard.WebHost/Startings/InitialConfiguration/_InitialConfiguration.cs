
namespace Bb.WebHost.Startings.InitialConfiguration
{


    public class _InitialConfiguration
    {

        public _InitialConfiguration()
        {
            //Web = new ServiceWeb();
            Loaders = new _Loaders();
        }

        public _Loaders Loaders { get; set; }

        ///// <summary>
        ///// Gets or sets the assemblie's list must be loaded at starting program.
        ///// </summary>
        ///// <value>
        ///// The assemblies.
        ///// </value>
        //public List<_InitializationAssemblyList> Builders { get; set; }


        public string Culture { get; set; }


        /// <summary>
        /// https://docs.microsoft.com/fr-fr/dotnet/api/system.datetimeoffset.tostring?view=netframework-4.7.2#System_DateTimeOffset_ToString_System_IFormatProvider_
        /// </summary>
        public string FormatDateCulture { get; set; }

        public bool UseSwagger { get; set; } = true;


        // public ServiceWeb Web { get; }


    }


}
