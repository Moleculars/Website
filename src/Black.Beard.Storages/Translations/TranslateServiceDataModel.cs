using System.Globalization;

namespace Bb.Translations
{
    public class TranslateServiceDataModel
    {

        public Guid _id { get; set; }

        public string[] Path { get; internal set; }

        public string Key { get; internal set; }

        public string Value { get; set; }

        public CultureInfo Culture { get; set; }

        public int Version { get; set; }

        public DateTime CreationDtm { get; set; }

        public DateTime LastUpdate { get; set; }

        public bool IsDirty { get; internal set; }

        public bool Local { get; set; }



        public string GetConcatPath
        {
            get
            {
                if (Path != null && Path.Length > 0)
                {
                    var result = String.Join(".", Path);
                    return result;
                }
                return String.Empty;
            }
        }



    }
}


