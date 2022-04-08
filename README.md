# MilestonePSToolsInCSharp

This is a rough example of how you can use a PowerShell runspace in a C# application, and specifically how you could call PowerShell scripts using MilestonePSTools.

It accepts command line arguments for the VMS server address, username, password, a flag for whether the user is a basic user, and finally an optional output path
where a camera report will be saved as a CSV.

The script is loaded from a PS1 file in the same folder since it was easier and cleaner to do that than to try to write a PowerShell script as a string in a cs file.

The plain text credentials from the command-line arguments are converted to a pscredential object using the powershell runspace with the `AddScript` method.

## IMPORTANT

**This example accepts a plain text username and password from the command-line. This is an awful practice and I beg you to never do this in production.**
