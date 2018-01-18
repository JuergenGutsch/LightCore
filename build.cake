#tool "nuget:?package=xunit.runner.console"

using Cake.Common.Tools.DotNetCore;

var target = Argument("target", "Default");


Task("Clean")
	.Does(() => {
		//DotNetCoreClean("./LightCore.sln");
	});

Task("NuGetRestore")
	.IsDependentOn("Clean")
	.Does(() => 
	{	
		// this hack is needed until 'dotnet restore' supports restoring
		// packages in mixed (.NET and .NET Core) solutions
		var projectFiles = GetFiles("./**/packages.config");
		var nugetOptions = new NuGetRestoreSettings{
			PackagesDirectory = "./packages"
		};
		foreach(var file in projectFiles)
		{
			if(file.FullPath.Contains("tools"))
			{
				continue;
			}
			NuGetRestore(file.FullPath, nugetOptions);
		}

		// 'dotnet restore'
		DotNetCoreRestore(); 
	});

Task("Build")
	.IsDependentOn("NuGetRestore")
	.Does(() => 
	{	
		// 'dotnet build'
		DotNetCoreBuild("./LightCore.sln", new DotNetCoreBuildSettings{
			Configuration = "Release"
		});
	});

Task("Test")
	.IsDependentOn("Build")
	.Does(() => {
		var settings = new DotNetCoreTestSettings
		{
			Configuration = "Release"
		};

		var projectFiles = GetFiles("./*.Tests/**/*.Tests.csproj");
		foreach(var file in projectFiles)
		{
			// 'dotnet test'
			DotNetCoreTest(file.FullPath, settings);
		}
	});

Task("Pack")
	.IsDependentOn("Test")
	.Does(() => {
		// comming soon
	});

Task("Default")
	.IsDependentOn("Pack")
	.Does(() =>
	{
	  Information("You build is done! :-)");
	});

RunTarget(target);