using Bb.WebClient.Startings;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;

namespace Bb.WebHost.Startings
{


    /// <summary>
    /// Parse a folder for discover all json config file and load them in memory
    /// </summary>
    public class FolderConfigurationLoader : IConfigurationLoader
    {

        public FolderConfigurationLoader()
        {

        }

        public void Load(InitializationLoader builder)
        {

            var root = Environment.CurrentDirectory;

            foreach (var configuration in Configuration.Paths)
            {

                var dir = new DirectoryInfo(Path.Combine(root, configuration));
                if (dir.Exists)
                {
                    foreach (var file in dir.GetFiles("*.json"))
                        if (EvaluateJson(file))
                        {
                            ConfigLoaded = true;
                            Trace.WriteLine($"add configuration file '{file.FullName}'", TraceLevel.Info.ToString());
                            builder.Builder.Configuration.AddJsonFile(file.FullName, optional: false, reloadOnChange: true);
                        }
                }
                else
                {
                    Trace.WriteLine($"directory '{dir.FullName}' not found", TraceLevel.Info.ToString());
                }
            }

        }


        private static bool EvaluateJson(FileInfo file)
        {

            if (file.Length == 0)
            {
                Trace.WriteLine($"configuration file '{file.FullName}' is empty", TraceLevel.Info.ToString());
                return false;
            }

            var txt = File.ReadAllText(file.FullName).Trim();
            if (string.IsNullOrEmpty(txt))
            {
                Trace.WriteLine($"configuration file '{file.FullName}' is empty", TraceLevel.Info.ToString());
                return false;
            }

            try
            {
                Newtonsoft.Json.Linq.JObject.Parse(File.ReadAllText(file.FullName));
            }
            catch (Exception e)
            {
                Trace.WriteLine($"configuration file '{file.FullName}' is failed ({e.Message})", TraceLevel.Error.ToString());
                return false;
            }

            return true;

        }


        public Loader Configuration { get; set; }

        public bool ConfigLoaded { get; private set; }

    }

}
