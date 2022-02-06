
using Bb.WebClient.UIComponents;
using MolecularSite.CommandLines;
using System.Diagnostics;

/*
    site run --e configConnexionString="Server=.;Database=BaseWebsite;Integrated Security=SSPI;Encrypt=true; TrustServerCertificate=true;"
    
 */

var cmd = Bb.CommandLine.Run<Command, CommandLine>(args);
//this.Result = cmd.Result;

// https://github.com/serdarciplak/BlazorMonaco
// https://microsoft.github.io/monaco-editor/