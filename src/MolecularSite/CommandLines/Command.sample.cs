//using Bb.CommandLines.Validators;
//using Microsoft.Extensions.CommandLineUtils;
//using Bb.CommandLines;

//namespace MolecularSite.CommandLines
//{
//    public partial class Command : Command<CommandLine>
//    {

//        /*
//            Syntax
//            .exe template execute "<template file>" --source:"value source" --debug
//        */

//        public CommandLineApplication CommandExecute(CommandLine app)
//        {

//            // The syntax start with template.
//            var cmd = app.Command("template", config =>
//            {
//                config.Description = "template process";
//                config.HelpOption(HelpFlag);
//            });

//            /*
//                json template execute -file '' -source
//            */
//            cmd.Command("execute", config =>
//            {

//                config.Description = "description of the command";
//                config.HelpOption(HelpFlag);

//                var validator = new GroupArgument(config);

//                // Add argument
//                var argTemplatePath = validator.Argument("<template file>", "template path"
//                    , ValidatorExtension.EvaluateFileExist
//                    , ValidatorExtension.EvaluateRequired
//                );

//                // Add argument
//                var argSource = validator.Option("--source", "json source path that contains data source"
//                    , ValidatorExtension.EvaluateFileExist
//                );

//                // Add option
//                var optwithDebug = validator.OptionNoValue("--debug", "activate debug mode");

//                config.OnExecute(() =>
//                {

//                    if (!validator.Evaluate(out int errorNum))
//                    {
//                        // add your code here.
//                    }

//                    return errorNum;

//                });
//            });


//            return app;

//        }

//    }

//}
