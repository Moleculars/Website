using Bb.CommandLines.Validators;
using Microsoft.Extensions.CommandLineUtils;
using Bb.CommandLines;

namespace MolecularSite.CommandLines
{
    public partial class Command : Command<CommandLine>
    {

   
        private static void RegisterEnvironmentValues(CommandOption values)
        {
            foreach (string item in values.Values)
            {

                var i = item.IndexOf('=');

                if (i > -1)
                {
                    string key = item.Substring(0, i).Trim();
                    string value = item.Substring(i + 1).Trim();
                    Environment.SetEnvironmentVariable(key, value);

                }


            }
        }
    }

}
