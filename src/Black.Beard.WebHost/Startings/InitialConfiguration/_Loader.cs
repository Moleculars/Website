using System.Collections.Generic;

namespace Bb.WebHost.Startings.InitialConfiguration
{

    public class _Loader
    {

        public _Loader()
        {
            Paths = new List<string>();
        }

        public string Type { get; set; }

        public List<string> Paths { get; set; }

        // public string ConnectionString { get; set; }


    }


}
