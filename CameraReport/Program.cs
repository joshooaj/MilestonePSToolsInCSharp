using CommandLine;
using System;
using System.IO;
using System.Management.Automation;

namespace CameraReport
{
  public class Options
  {
    [Option('a', "ServerAddress", Required = true, HelpText = "VMS management server address in the form of a URL.")]
    public Uri ServerAddress { get; set; }
    
    [Option('u', "Username", Required = true, HelpText = "VMS username.")]
    public string Username { get; set; }

    [Option('p', "Password", Required = true, HelpText = "VMS user password.")]
    public string Password { get; set; }

    [Option('b', "BasicUser", Required = false, HelpText = "Specifies that the VMS credentials are for a basic user.")]
    public bool BasicUser { get; set; }

    [Option('o', "OutputPath", Required = false, HelpText = "Specifies a path, including file name, to save a camera report CSV file.")]
    public string OutputPath { get; set; }
  }

  internal class Program
  {
    static void Main(string[] args)
    {

      Parser.Default.ParseArguments<Options>(args)
        .WithParsed<Options>(o =>
        {
          RunCameraReport(o.ServerAddress, o.Username, o.Password, o.BasicUser, o.OutputPath);
        });
    }

    private static void RunCameraReport(Uri serverAddress, string userName, string password, bool basicUser, string outputPath)
    {
      var script = File.ReadAllText(@".\CameraReport.ps1");
      using (var powershell = PowerShell.Create())
      {
        var cred = powershell.AddScript($"[pscredential]::new('{userName}', ('{password}' | ConvertTo-SecureString -AsPlainText -Force))").Invoke()[0];
        powershell.Commands.Clear();
        var result = powershell
          .AddScript(script)
          .AddArgument(serverAddress)
          .AddArgument(cred)
          .AddArgument(basicUser)
          .AddArgument(outputPath)
          .Invoke();
        foreach (var row in result)
        {
          Console.WriteLine(row);
        }
      }
    }
  }
}
