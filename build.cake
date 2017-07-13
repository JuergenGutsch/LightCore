var target = Argument("target", "Default");

Task("NuGetRestore")
	.Does(() => 
	{	
		NuGetRestore("./LightCore.sln");
	});

Task("DotNetBuild")
	.IsDependentOn("NuGetRestore")
	.Does(() => 
	{	
		MSBuild("./LightCore.sln");
	});

Task("DotNetTest")
	.IsDependentOn("DotNetBuild")
	.Does(() => {
	
	});

Task("DotNetPack")
	.IsDependentOn("DotNetTest")
	.Does(() => {
	
	});

Task("Default")
	.IsDependentOn("DotNetPack")
	.Does(() =>
	{
	  Information("You build is done! :-)");
	});

RunTarget(target);