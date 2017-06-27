var target = Argument("target", "Default");

Task("NuGetRestore")
	.Does(() => 
	{	
		NuGetRestore("./LightCore.sln");
	});

Task("Build")
	.IsDependentOn("NuGetRestore")
	.Does(() => 
	{	
		MSBuild("./LightCore.sln");
	});

Task("Default")
	.IsDependentOn("Build")
	.Does(() =>
	{
	  Information("You build is done!");
	});

RunTarget(target);