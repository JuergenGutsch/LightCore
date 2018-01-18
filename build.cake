#tool "nuget:?package=xunit.runner.console"

var target = Argument("target", "Default");

Task("NuGetRestore")
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

Task("DotNetBuild")
	.IsDependentOn("NuGetRestore")
	.Does(() => 
	{	
		// 'dotnet build'
		DotNetCoreBuild("./LightCore.sln", new DotNetCoreBuildSettings{
			Configuration = "Release"
		});
	});

Task("DotNetTest")
	.IsDependentOn("DotNetBuild")
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

Task("DotNetPack")
	.IsDependentOn("DotNetTest")
	.Does(() => {
		
		var settings = new DotNetCorePackSettings
		{
			Configuration = "Release",
			OutputDirectory = "./artifacts/"
		};

		DotNetCorePack("./LightCore.sln", settings);
	});

Task("DotNetPush")
	.IsDependentOn("DotNetPack")
	.Does(() => {
		
		//

	});

Task("Default")
	.IsDependentOn("DotNetPush")
	.Does(() =>
	{
	  Information("You build is done! :-)");
	});

RunTarget(target);