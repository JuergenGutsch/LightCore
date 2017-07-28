#tool "nuget:?package=xunit.runner.console"

var target = Argument("target", "Default");

Task("NuGetRestore")
	.Does(() => 
	{	
		DotNetCoreRestore(); 
	});

Task("DotNetBuild")
	.IsDependentOn("NuGetRestore")
	.Does(() => 
	{	
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
			DotNetCoreTest(file.FullPath, settings);
		}
	});

Task("DotNetPack")
	.IsDependentOn("DotNetTest")
	.Does(() => {
		// comming soon
	});

Task("Default")
	.IsDependentOn("DotNetPack")
	.Does(() =>
	{
	  Information("You build is done! :-)");
	});

RunTarget(target);