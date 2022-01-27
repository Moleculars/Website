using System.Collections.Generic;

namespace Bb.WebClient.Startings
{

    public class Loader
    {

        public Loader()
        {
            Paths = new List<string>();
        }

        public string? Type { get; set; }

        public List<string> Paths { get; set; }

        // public string ConnectionString { get; set; }


    }


}
