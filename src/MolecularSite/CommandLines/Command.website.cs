using Bb.CommandLines.Validators;
using Microsoft.Extensions.CommandLineUtils;
using Bb.CommandLines;

namespace MolecularSite.CommandLines
{
    public partial class Command : Command<CommandLine>
    {

        /*
            Syntax
            .exe site run --e arg1:value1
        */

        public CommandLineApplication CommandExecute(CommandLine app)
        {


            var cmd = app.Command("site", config =>
            {
                config.Description = "manage web site";
                config.HelpOption(HelpFlag);
            });


            cmd.Command("run", config =>
            {

                config.Description = "run web site";
                config.HelpOption(HelpFlag);

                var validator = new GroupArgument(config);

                // Add argument
                //var argTemplatePath = validator.Argument("<template file>", "template path"
                //    , ValidatorExtension.EvaluateFileExist
                //    , ValidatorExtension.EvaluateRequired
                //);

                // Add option
                //var applicationName = validator.Option("--applicationName", "Name of the application");
                //var contentRootPath = validator.Option("--contentRootPath", "content root path");
                //var environmentName = validator.Option("--environmentName", "environment name");
                //var webRootPath = validator.Option("--webRootPath", "web root path");


                var environmentValues = validator.OptionMultiValue("--e", "environment name");


                config.OnExecute(() =>
                {

                    if (!validator.Evaluate(out int errorNum)) { }

                    if (environmentValues.HasValue())
                        RegisterEnvironmentValues(environmentValues);

                    var site = new WebSite(Environment.GetCommandLineArgs());
                    site.RunSite();

                    return errorNum;

                });
            });


            return app;

        }

    }

}
