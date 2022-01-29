
using MolecularSite.CommandLines;

/*
 
   site run --e configConnexionString="Server=.;Database=BaseWebsite;Integrated Security=SSPI;Encrypt=true; TrustServerCertificate=true;"

 */

var cmd = Bb.CommandLine.Run<Command, CommandLine>(args);
//this.Result = cmd.Result;

