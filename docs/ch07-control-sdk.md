# Controlling the .NET SDK

By default, executing `dotnet` commands uses the highest version of the .NET SDK installed. There may be times when you want to control which SDK is used.

For example, once .NET 12 becomes available in preview, starting in February 2027, or the final version becomes available in November 2027, you might install it. But you would probably want your experience to match the book steps, which use the .NET 11 SDK. But once you install a .NET 12 SDK, it will be used by default.

You can control the .NET SDK used by default in a folder hierarchy by using a `global.json` file, which defines the SDK version to use. The `dotnet` command searches the current folder and then each ancestor folder in turn for a `global.json` file, seeing whether it should use a different .NET SDK version.

> You do not need to complete the following steps, but if you want to try and do not already have .NET 10 SDK installed, then you can install it from the following link: https://dotnet.microsoft.com/download/dotnet/10.0.

Let’s do it:
1.	Create a subdirectory/folder in the `Chapter07` folder named `ControlSDK`.
2.	On Windows, start **Command Prompt** or **Windows Terminal**. On macOS, start **Terminal**. If you are using VS Code, then you can use the integrated terminal.
3.	In the `ControlSDK` folder, at the command prompt or terminal, list the installed .NET SDKs:
```shell
dotnet --list-sdks
```
4.	Note the results and the version number of the latest .NET 10 SDK installed:
```
...
10.0.301 [MP1.1][C:\Program Files\dotnet\sdk]
11.0.100 [C:\Program Files\dotnet\sdk]
```
5.	at the command prompt or terminal, create a `global.json` file that forces the use of the latest .NET 10 SDK that you have installed (which might be later than mine):
```shell
dotnet new globaljson --sdk-version 10.0.301
```
6.	Note the result:
```
The template "global.json file" was created successfully.
```
7.	Use your preferred code editor to open the `global.json` file. For example, to open it with VS Code, enter the `code global.json` command. Note the version of the SDK that will be used in this folder and subfolders:
```json
{
  "sdk": {
    "version": "10.0.301"
  }
}
```
8.	In the `ControlSDK` folder, at the command prompt or terminal, create a class library project:
```
dotnet new classlib
```

> **Warning!** If you do not have the .NET 10 SDK installed, then you will see an error:
> `Could not execute because the application was not found or a compatible .NET SDK is not installed.`

10.	If you do have the .NET 10 SDK installed, then a class library project will be created that targets .NET 10 by default:
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```
